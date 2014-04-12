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

namespace Feedbook.Phone7.Automation
{
    public class ThemeIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var relativeUri = (value ?? parameter) as string;
            if (relativeUri != null)
            {
                var baseUri = ((Visibility)App.Current.Resources["PhoneDarkThemeVisibility"]) == Visibility.Visible ? "/Theme/Dark/" : "/Theme/Light/";

                return new Uri(new Uri(baseUri), relativeUri);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
