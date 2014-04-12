using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Feedbook.Phone7.Helper;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading;
using WPCore.ValueTypeExtension;
using System.Collections.ObjectModel;
using Feedbook.Phone7.Model;
using Feedbook.Phone7.Services.WebFeed;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Shell;
using WPCore.RefTypeExtension;


namespace Feedbook.Phone7.Views
{
    public partial class DiscoveryPage : PhoneApplicationPage
    {
        private static readonly Regex LinkRegex = new Regex(Constants.ANCHOR_LINK_TAG_REGEX, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex HrefRegex = new Regex(Constants.HREF_ATTRIBUTE_REGEX, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex TypeRegex = new Regex(Constants.TYPE_ATTRIBUTE_REGEX, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private Queue<string> urls = new Queue<string>();
        private ObservableCollection<Channel> channels = new ObservableCollection<Channel>();

        public DiscoveryPage()
        {
            InitializeComponent();
            this.ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().ForEach(b => b.IconUri = Helper.HelperFunction.GetThemeIcon(b.IconUri.OriginalString));
            this.channels.CollectionChanged += (sender, e) => this.ApplicationBar.IsVisible = this.channels.Count > 0;
            this.ChannelListBox.ItemsSource = this.channels;
            if (this.UrlTextBox.Text != null)
                this.UrlTextBox.SelectionStart = this.UrlTextBox.Text.Length;
        }

        private void UrlTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.UrlTextBox.Text != null && this.UrlTextBox.Text.Trim().Length > 0)
            {
                this.Focus();
                string url = this.UrlTextBox.Text.Trim();

                //Checking for valid url
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    this.urls.Clear();
                    this.urls.Enqueue(url);
                    this.channels.Clear();
                    this.DiscoverUrl();
                }
                else
                    MessageBox.Show("Url is not valid!");
            }
        }

        private void DiscoverUrl()
        {
            if (this.urls.Count > 0)
            {
                this.ProgressBar.IsIndeterminate = true;
                Downloader.DownloadStringAsync(urls.Dequeue(), this.DownloadProgressChanged, this.DownloadCompleted);
            }
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.TotalBytesToReceive != -1)
                this.ProgressBar.Value = e.ProgressPercentage;
            else if (!this.ProgressBar.IsIndeterminate)
                this.ProgressBar.IsIndeterminate = true;
        }

        private void DownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (this.ProgressBar.IsIndeterminate)
                this.ProgressBar.IsIndeterminate = false;
            else
                this.ProgressBar.Value = 0;

            if (e.Cancelled)
            {
                MessageBox.Show("Download cancelled!", Constants.Caption.INFO, MessageBoxButton.OK);
            }
            else if (e.Error != null)
            {
                if (this.channels.Count == 0)
                    MessageBox.Show(e.Error.Message);
            }
            else
            {
                var url = e.UserState as string;
                DiscoverContent(e.Result, url);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void DiscoverContent(string content, string downloadUrl)
        {
            try
            {
                XElement element = null;
                try { element = XElement.Parse(content, LoadOptions.PreserveWhitespace); }
                catch { }

                if (element == null || string.Compare(element.Name.LocalName, "html", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    foreach (Match linkMatch in LinkRegex.Matches(content))
                    {
                        Group urlGroup;
                        Group typeGroup;
                        var hrefMatch = HrefRegex.Match(linkMatch.Value);
                        var typeMatch = TypeRegex.Match(linkMatch.Value);

                        if (hrefMatch.Success && typeMatch.Success
                            && (urlGroup = hrefMatch.Groups["url"]) != null
                            && (typeGroup = typeMatch.Groups["type"]) != null
                            && Uri.IsWellFormedUriString(urlGroup.Value, UriKind.Absolute)
                            && typeGroup.Value.In("application/rss+xml", "application/atom+xml")
                            && !urls.Contains(urlGroup.Value)
                            && ((urlGroup.Value + "").ToLower().StartsWith("http://")
                                 || (urlGroup.Value + "").ToLower().StartsWith("https://")))
                            urls.Enqueue(urlGroup.Value);
                    }

                    if (urls.Count == 0)
                        MessageBox.Show("No feed links found in this page!");
                }
                else
                {

                    //var account = this.view.Account;
                    this.Cursor = Cursors.Wait;
                    this.ProgressBar.IsIndeterminate = true;

                    ThreadPool.QueueUserWorkItem(new WaitCallback(
                        (object o) =>
                        {
                            try
                            {
                                int index;
                                if (content != null && (index = content.IndexOf('<')) != -1 && index != 0)
                                    content = content.Substring(index);

                                var channel = (new FeedProcessor()).Parse(content, downloadUrl);
                                channel.Feeds
                                       .OrderByDescending(f => f.Updated)
                                       .Skip(SysConfig.MaxWebFeed)
                                       .ToArray()
                                       .ForEach(f => channel.Feeds.Remove(f));
                                if (!this.channels.Any(s => s.ChannelId == channel.ChannelId) && !Storage.Channels.Any(c => c.ChannelId == channel.ChannelId))
                                    this.Dispatcher.BeginInvoke(new Action(() => this.channels.Add(channel)));
                            }
                            catch (Exception ex)
                            {
                                this.Dispatcher.BeginInvoke(new Action(() => HelperFunction.HandleException("WOops! error occurred while parsing feed channel", ex)));
                            }
                            finally
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    this.ProgressBar.IsIndeterminate = false;
                                    this.ProgressBar.Value = 0;
                                    this.Cursor = Cursors.Arrow;
                                }));
                            }
                        }));
                }
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }

            DiscoverUrl();
        }

        private void Done_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var checkbox in this.ChannelListBox.GetVisualDescendants().OfType<CheckBox>().Where(c => c.IsChecked == true))
                    if (checkbox.DataContext is Channel)
                    {
                        var channel = (Channel)checkbox.DataContext;
                        if (!Storage.Channels.Any(c => c.ChannelId == channel.ChannelId))
                            Storage.Channels.Add(channel);
                    }

                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.UrlTextBox.UpdateLayout();
            this.UrlTextBox.Focus();
        }
    }
}