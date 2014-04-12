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

namespace Feedbook.Phone7.Automation
{
    public class ContentPreviewConverter : ContentToPainTextConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var content = base.Convert(value, targetType, parameter, culture);
            if (content != null && content is string)
            {
                var length = 200;
                var strContent = content as string;

                strContent = string.Join("\n", strContent.Split('\n').Where(c => !String.IsNullOrWhiteSpace(c)).ToArray()); //Compact(Compact(strContent, " "), "\n");

                try { length = System.Convert.ToInt32(parameter ?? 100); }
                catch { }
                
                return strContent.Length > length ? strContent.Substring(0, length) + "..." : strContent;
            }

            return content;
        }

        private static string Compact(string content, string value)
        {
            var mvalue = value + value;
            while (content.IndexOf(mvalue) != -1) content = content.Replace(mvalue, value);

            return content;
        }
    }
}
