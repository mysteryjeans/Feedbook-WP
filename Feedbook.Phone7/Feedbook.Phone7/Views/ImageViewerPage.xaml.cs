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
using System.Windows.Media.Imaging;

namespace Feedbook.Phone7.Views
{
    public partial class ImageViewerPage : PhoneApplicationPage
    {
        public ImageViewerPage()
        {
            InitializeComponent();           
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.imageListBox.ItemsSource = this.GetSession("Images") as string[];
            this.imageListBox.SelectedItem  = this.GetSession("SelectedImage") as string;
        }

        private void imageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedImage = this.imageListBox.SelectedItem as string;
            if (selectedImage != null)
            {
                this.imageBorder.Show();
                Uri imageSource = new Uri(selectedImage, selectedImage.StartsWith("http://") ? UriKind.Absolute : UriKind.RelativeOrAbsolute);
                this.image.Source = new BitmapImage(imageSource);
            }
            else
            {
                this.imageBorder.Hide();
                this.image.Source = null;
            }
        }

        private void Cross_Click(object sender, RoutedEventArgs e)
        {
            this.imageListBox.SelectedIndex = -1;
        }
    }
}