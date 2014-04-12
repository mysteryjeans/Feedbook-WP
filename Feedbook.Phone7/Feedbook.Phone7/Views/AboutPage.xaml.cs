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
using System.Reflection;
using Feedbook.Phone7.Helper;
using Microsoft.Phone.Tasks;

namespace Feedbook.Phone7.Views
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
            var assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            this.tbVersions.Text = assemblyName.Version.Major + "." + assemblyName.Version.Minor;
        }

        private void Website_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelperFunction.OpenInBrowser(Constants.WEBSITE_URL);
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }
        }

        private void Rate_Review_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new MarketplaceReviewTask().Show();
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var emailTask = new EmailComposeTask { To = Constants.SUPPORT_EMAIL };
                emailTask.Show();
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }
        }
    }
}