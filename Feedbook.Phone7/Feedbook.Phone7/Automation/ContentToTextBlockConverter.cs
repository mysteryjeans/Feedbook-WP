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
using Microsoft.Phone.Tasks;

namespace Feedbook.Phone7.Automation
{
    public class ContentToTextBlockConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var resources = Application.Current.Resources;

            string valueStr = value as string;
            //return new TextBlock { Text = valueStr ?? string.Empty, FontSize = (double)resources["PhoneFontSizeNormal"], TextWrapping = TextWrapping.Wrap };

            WrapPanel panel = new WrapPanel { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
            string[] lines = valueStr.Split('\n');
            int count = 0;
            foreach (var line in lines)
            {
                count++;
                string sentence = null;
                string[] words = line.Split(' ');
                int wordcount = 0;
                foreach (var word in words)
                {
                    if ((word.StartsWith("http://") || word.StartsWith("https://"))
                        && Uri.IsWellFormedUriString(word, UriKind.Absolute))
                    {
                        if (sentence != null)
                        {
                            sentence += " ";
                            panel.Children.Add(new TextBlock { Text = sentence, FontSize = 24, TextWrapping = TextWrapping.Wrap });
                            sentence = null;
                        }
                        //panel.Children.Add(new HyperlinkButton { NavigateUri = new Uri(word), Content = word, FontSize = (double)resources["PhoneFontSizeNormal"] });
                        var button = new Button { Template = (ControlTemplate)resources["TransparentButtonTemplate"], BorderThickness = new Thickness(0), Padding = new Thickness(0), Margin = new Thickness(0), Content = word, FontSize = 24 };
                        button.Click += delegate
                        {
                            WebBrowserTask task = new WebBrowserTask();
                            task.Uri = new Uri(button.Content.ToString());
                            task.Show();
                        };

                        panel.Children.Add(button);
                    }
                    else
                    {
                        if (wordcount > 0)
                            sentence += " " + word;
                        else
                            sentence = word;
                    }
                    wordcount++;
                }
                if (count < lines.Length)
                    sentence += "\n";

                if (sentence != null)
                    panel.Children.Add(new TextBlock { Text = sentence, FontSize = 24, TextWrapping = TextWrapping.Wrap });
            }

            return panel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
