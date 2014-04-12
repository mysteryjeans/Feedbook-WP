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

namespace Feedbook.Phone7.Views
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.PanoramaFeedView.IsChecked = SysConfig.PanoramaFeedView;
        }

        private void ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            SysConfig.PanoramaFeedView = this.PanoramaFeedView.IsChecked.GetValueOrDefault();
        }
    }
}