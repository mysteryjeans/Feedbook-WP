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
using Feedbook.Phone7.Model;
using Microsoft.Phone.Shell;
using WPCore.RefTypeExtension;
using Microsoft.Phone.Tasks;

namespace Feedbook.Phone7.Views
{
    public partial class FeedPage : PhoneApplicationPage
    {
        public FeedPage()
        {
            InitializeComponent();
            this.ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().ForEach(b => b.IconUri = Helper.HelperFunction.GetThemeIcon(b.IconUri.OriginalString));
            var channel = this.GetSession("Channel") as Channel;
            var feed = this.GetSession("Feed") as Feed;
            if (channel != null && feed != null)
            {
                this.DataContext = channel;
                this.FeedGrid.DataContext = feed;
                this.ChannelTitle.Text = channel.Title.ToPlainText;
                //this.webBrowser.NavigateToString(feed.EncodedDescription);
            }
        }

        private void OpenLink_Click(object sender, EventArgs e)
        {
            var feed = this.FeedGrid.DataContext as Feed;
            if (feed != null)
                try
                {
                    HelperFunction.OpenInBrowser(feed.Link.HRef);
                }
                catch (Exception ex)
                {
                    HelperFunction.HandleException(ex);
                }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            var feed = this.FeedGrid.DataContext as Feed;
            var button = sender as ApplicationBarIconButton;
            if (button != null && feed != null)
                try
                {
                    switch (button.Text)
                    {
                        case "Share":
                            ShareLinkTask shareTask = new ShareLinkTask { LinkUri = new Uri(feed.Link.HRef), Title = feed.Title, Message = HelperFunction.Ellipsis(feed.TextDescription, SysConfig.MaxPostLength) };
                            shareTask.Show();
                            break;
                        case "Newer":
                            this.NewFeed();
                            break;
                        case "Older":
                            this.OldFeed();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HelperFunction.HandleException(ex);
                }
        }

        private void NewFeed()
        {
            try
            {
                var channel = this.DataContext as Channel;
                var feed = this.FeedGrid.DataContext as Feed;
                if (feed != null)
                {
                    var newFeed = channel.Feeds.Where(f => f.Updated > feed.Updated).OrderBy(f => f.Updated).FirstOrDefault();
                    if (newFeed != null)
                    {
                        VisualStateManager.GoToState(this, "SlideRightOut", false);
                        this.BeginInvoke(() =>
                        {
                            this.FeedGrid.DataContext = newFeed;
                            VisualStateManager.GoToState(this, "SlideRightIn", false);
                        }, 250);
                    }
                    else
                        this.NavigationService.GoBack();

                    feed.IsReaded = true;
                }
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }
        }

        private void OldFeed()
        {
            try
            {
                var channel = this.DataContext as Channel;
                var feed = this.FeedGrid.DataContext as Feed;
                if (feed != null)
                {
                    var oldFeed = channel.Feeds.Where(f => f.Updated < feed.Updated).OrderByDescending(f => f.Updated).FirstOrDefault();
                    if (oldFeed != null)
                    {
                        VisualStateManager.GoToState(this, "SlideLeftOut", false);
                        this.BeginInvoke(() =>
                        {
                            this.FeedGrid.DataContext = oldFeed;
                            VisualStateManager.GoToState(this, "SlideLeftIn", false);
                        }, 250);
                    }
                    else
                        this.NavigationService.GoBack();

                    feed.IsReaded = true;
                }
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            var feed = this.FeedGrid.DataContext as Feed;
            if (feed != null)
                feed.IsReaded = true;
        }
    }
}