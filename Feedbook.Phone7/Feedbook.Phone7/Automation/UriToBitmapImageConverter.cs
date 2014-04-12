using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Feedbook.Phone7.Automation
{
    public class UriToBitmapImageConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string valueStr = value as string;
            if (!string.IsNullOrEmpty(valueStr))
            {
                Uri imageSource = new Uri(valueStr, valueStr.StartsWith("http://") ? UriKind.Absolute : UriKind.RelativeOrAbsolute);
                return new BitmapImage(imageSource);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
