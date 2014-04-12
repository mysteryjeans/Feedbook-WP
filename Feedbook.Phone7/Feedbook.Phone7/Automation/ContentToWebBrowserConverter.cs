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
using System.Windows.Data;
using Microsoft.Phone.Controls;

namespace Feedbook.Phone7.Automation
{
    public class ContentToWebBrowserConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string valueString = value as string;
            var browser = new WebBrowser { Background = null, IsScriptEnabled = false, Height=200};
            browser.VerticalAlignment = VerticalAlignment.Top;
            browser.Loaded += delegate
            {
                browser.NavigateToString(valueString);
            };
            return browser;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
