using System;
using System.Linq;
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
using Feedbook.Phone7.Model;
using System.Collections.Generic;

namespace Feedbook.Phone7.Automation
{
    public class FlashEnclosuresConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var enclosures = value as IEnumerable<Enclosure>;
            if (enclosures != null)
                return enclosures.Where(e => e.Type != null && e.Type == "application/x-shockwave-flash");

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
