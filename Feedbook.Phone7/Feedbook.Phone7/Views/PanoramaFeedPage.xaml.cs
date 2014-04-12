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
using Microsoft.Phone.Shell;
using WPCore.RefTypeExtension;
using Feedbook.Phone7.Model;
using Microsoft.Phone.Tasks;

namespace Feedbook.Phone7.Views
{
    public partial class PanoramaFeedPage : PhoneApplicationPage
    {
        public PanoramaFeedPage()
        {
            InitializeComponent();
            this.ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().ForEach(b => b.IconUri = Helper.HelperFunction.GetThemeIcon(b.IconUri.OriginalString));
            this.DataContext = this.GetSession("Channel");
            this.PanoramaView.DefaultItem = this.GetSession("Feed");
        }

        private void OpenLink_Click(object sender, EventArgs e)
        {
            var feed = this.PanoramaView.SelectedItem as Feed;
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
            var feed = this.PanoramaView.SelectedItem as Feed;
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
                    }
                }
                catch (Exception ex)
                {
                    HelperFunction.HandleException(ex);
                }
        }

        private void PanoramaView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems != null)
                foreach (Feed feed in e.RemovedItems)
                    feed.IsReaded = true;
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            var feed = this.PanoramaView.SelectedItem as Feed;
            if (feed != null)
                feed.IsReaded = true;
        }
    }
}