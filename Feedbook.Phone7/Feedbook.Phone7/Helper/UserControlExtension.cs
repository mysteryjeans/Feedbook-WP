using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Linq;
using System.Windows.Controls;

namespace Feedbook.Phone7.Helper
{
    public static class UserControlExtension
    {
        public static Page GetPage(this UserControl control)
        {
            return control.GetVisualAncestors()
                          .OfType<Page>()
                          .First();
        }
    }
}
