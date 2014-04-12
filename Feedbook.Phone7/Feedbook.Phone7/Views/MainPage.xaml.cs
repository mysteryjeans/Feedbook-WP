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
using Feedbook.Phone7.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WPCore.RefTypeExtension;
using WPCore.ValueTypeExtension;
using Feedbook.Phone7.Services;
using Feedbook.Phone7.Helper;
using Microsoft.Phone.Tasks;
using System.Windows.Data;
using Feedbook.Phone7.Automation;
using Microsoft.Phone.Shell;

namespace Feedbook.Phone7.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().ForEach(b => b.IconUri = Helper.HelperFunction.GetThemeIcon(b.IconUri.OriginalString));
            this.ReaderView.AddHandler(Button.TapEvent, new EventHandler<System.Windows.Input.GestureEventArgs>(this.Feed_Click), false);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (Storage.Channels.Count == 0)
                this.Navigate(Constants.Page.FEED_DISCOVERY);
            else
                base.OnNavigatedTo(e);

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Storage.Favorites.Count > 0)
            {
                if (!this.ReaderView.Items.Contains(this.pivotTags))
                    this.ReaderView.Items.Add(this.pivotTags);
            }
            else if (this.ReaderView.Items.Contains(this.pivotTags))
                this.ReaderView.Items.Remove(this.pivotTags);
        }

        private void Feed_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var feed = HelperFunction.DataContextAs<Feed>(e.OriginalSource);
            if (feed != null)
            {
                var channel = Storage.Channels.FirstOrDefault(c => c.Feeds.Contains(feed));
                if (channel != null)
                {
                    this.SetSession("Feed", feed);
                    this.SetSession("Channel", channel);
                    if (SysConfig.PanoramaFeedView)
                        this.Navigate(Constants.Page.PANORAMA_FEED_PAGE);
                    else
                        this.Navigate(Constants.Page.FEED_PAGE);
                }
            }
        }

        private void Feedbook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var pivotItem = e.AddedItems.OfType<PivotItem>().FirstOrDefault();
            //if (pivotItem != null)
            //{
            //    this.ApplicationBar.Buttons.OfType<ApplicationBarIconButton>().ForEach(b => b.IsEnabled = false);
            //    switch (pivotItem.Header.ToString())
            //    {
            //        case "subscriptions":
            //            this.ApplicationBar.Buttons.OfType<ApplicationBarIconButton>().Where(b => b.Text.In("Add", "Edit")).ForEach(b => b.IsEnabled = true);
            //            break;
            //        case "tags":
            //        case "favorites":
            //            this.ApplicationBar.Buttons.OfType<ApplicationBarIconButton>().First(b => b.Text == "Favorite").IsEnabled = true;
            //            break;
            //    }
            //}
        }

        private void ApplicationBarButton_Click(object sender, EventArgs e)
        {
            var button = sender as ApplicationBarIconButton;
            if (button != null)
                switch (button.Text)
                {
                    case "Add":
                        this.Navigate(Constants.Page.FEED_DISCOVERY);
                        break;
                    case "Edit":
                        this.Navigate(Constants.Page.SUBSCRIPTIONS_PAGE);
                        break;
                    case "Tags":
                        this.Navigate(Constants.Page.TAGS_PAGE);
                        break;
                }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            var button = sender as ApplicationBarMenuItem;
            if (button != null)
                switch (button.Text)
                {
                    case "Settings":
                        this.Navigate(Constants.Page.SETTINGS);
                        break;
                    case "About":
                        this.Navigate(Constants.Page.ABOUT);
                        break;
                }
        }
    }
}