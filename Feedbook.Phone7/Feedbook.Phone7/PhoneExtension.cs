using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace Feedbook.Phone7
{
    public static class PhoneExtension
    {
        public static bool Navigate(this UserControl user, Uri uri)
        {
            var page = user.GetVisualAncestorsAndSelf()
                           .OfType<Page>()
                           .FirstOrDefault();

            if (page != null)
                return page.NavigationService.Navigate(uri);

            return false;
        }
    }
}
