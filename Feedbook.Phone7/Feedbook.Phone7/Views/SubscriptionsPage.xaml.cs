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
using Microsoft.Phone.Shell;
using WPCore.RefTypeExtension;
using System.Windows.Controls.Primitives;

namespace Feedbook.Phone7.Views
{
    public partial class SubscriptionsPage : PhoneApplicationPage
    {
        public SubscriptionsPage()
        {
            InitializeComponent();
            this.ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().ForEach(b => b.IconUri = Helper.HelperFunction.GetThemeIcon(b.IconUri.OriginalString));
            this.Subscriptions.ItemsSource = Storage.Channels;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {            
            foreach (var checkBox in this.Subscriptions.GetVisualDescendants().OfType<CheckBox>().Where(c => c.IsChecked == true))
                Storage.Channels.Remove(checkBox.DataContext as Channel);

            this.NavigationService.GoBack();
        }
    }
}