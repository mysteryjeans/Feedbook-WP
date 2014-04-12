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

namespace Feedbook.Phone7.Views
{
    public partial class FlashViewerPage : PhoneApplicationPage
    {
        public FlashViewerPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.flashListBox.ItemsSource = this.GetSession("FlashVideos") as Enclosure[];
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.flashListBox.SelectedItem = this.GetSession("SelectedFlashVideo") as Enclosure;
        }

        private void flashListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Enclosure selectedVideo = this.flashListBox.SelectedItem as Enclosure;
            if (selectedVideo != null)
            {
                this.flashBorder.Show();
                this.browser.Navigate(new Uri(selectedVideo.Url, UriKind.Absolute));
            }
            else
            {
                this.flashBorder.Hide();;
                //this.browser.Navigate("about:blank");
            }
        }

        private void Cross_Click(object sender, RoutedEventArgs e)
        {
            this.flashListBox.SelectedIndex = -1;
        }      
    }
}