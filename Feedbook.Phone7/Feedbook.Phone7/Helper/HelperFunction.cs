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
using System.Collections.Generic;
using WPCore.Util;
using System.Threading;
using System.IO;
using System.Windows.Threading;
using WPCore.ValueTypeExtension;
using Microsoft.Phone.Tasks;
using System.Windows.Controls.Primitives;

namespace Feedbook.Phone7.Helper
{
    public static class HelperFunction
    {
        public static Uri GetThemeIcon(string relativeUri)
        {
            var baseUri = ((Visibility)App.Current.Resources["PhoneDarkThemeVisibility"]) == Visibility.Visible ? "/Theme/Dark/" : "/Theme/Light/";

            return new Uri(baseUri + relativeUri, UriKind.Relative);
        }

        public static T DataContextAs<T>(object frameworkElement) where T : class
        {
            FrameworkElement element = frameworkElement as FrameworkElement;
            return element != null ? element.DataContext as T : null;
        }

        public static void BeginInvoke(Dispatcher dispatcher, Action action, TimeSpan wait)
        {
            BeginInvoke(new Action(() => dispatcher.BeginInvoke(action)), wait);
        }

        public static void BeginInvoke(Dispatcher dispatcher, Action action)
        {
            BeginInvoke(new Action(() => dispatcher.BeginInvoke(action)), TimeSpan.Zero);
        }

        public static void BeginInvoke(Action action, TimeSpan wait)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
            {
                if (wait != TimeSpan.Zero)
                    Thread.Sleep(wait);

                action();
            }));
        }

        public static void ShowPopup(UIElement parentElement)
        {
            if (parentElement != null)
            {
                var popup = parentElement.GetVisualDescendants()
                                         .OfType<Popup>()
                                         .FirstOrDefault();

                if (popup != null)
                {
                    popup.IsOpen = true;
                    popup.Show(Constants.Default.ShowMilliseconds);
                    parentElement.BeginInvoke((() => popup.Hide(Constants.Default.HideMilliseconds)), Constants.Default.PopupDurationMilliseconds);
                }
            }
        }

        public static void HidePopup(UIElement childElement)
        {
            var popup = childElement.GetVisualAncestors()
                              .OfType<Popup>()
                              .FirstOrDefault();

            if (popup != null)
                popup.IsOpen = false;
        }

        public static Dispatcher GetDispatcher()
        {
            return App.Dispatcher;
        }

        public static Uri GetLastUri(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text[text.Length - 1].In(' ', ',', '\n', ';', '\t'))
                {
                    string[] words = text.Split(' ', ',', '\n', ';', '\t');
                    if (words != null && words.Length > 1)
                    {
                        string lastWord = words[words.Length - 2];
                        if (lastWord != null && lastWord.StartsWith("http") && Uri.IsWellFormedUriString(lastWord, UriKind.Absolute))
                            return new Uri(lastWord);
                    }
                }
            }

            return null;
        }

        public static void OpenInBrowser(string url)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new Uri(url);
            task.Show();
        }

        public static string Ellipsis(string value, int length)
        {
            if (value != null && value.Length > length)
                return value.Left(length - 3) + "...";

            return value;
        }

        public static void HandleException(string message, Exception ex)
        {
            MessageBox.Show(string.Format("{1}{0}{0}Error:{0}{2}", Environment.NewLine, message, ex.Message));
        }

        public static void HandleException(Exception ex)
        {
            HandleException("WOops! something went wrong", ex);
        }

        public static void HandleBackgroundException(Exception ex)
        {
            HandleBackgroundException(ex.Message, ex);
        }

        public static void HandleBackgroundException(string message, Exception ex)
        {
            HelperFunction.BeginInvoke(App.Dispatcher, () =>
            {
                if (Microsoft.Phone.Shell.SystemTray.IsVisible)
                {
                    if (Microsoft.Phone.Shell.SystemTray.ProgressIndicator == null)
                        Microsoft.Phone.Shell.SystemTray.ProgressIndicator = new Microsoft.Phone.Shell.ProgressIndicator();

                    var indicator = Microsoft.Phone.Shell.SystemTray.ProgressIndicator;
                    indicator.Text = message;
                    indicator.IsVisible = true;
                    HelperFunction.BeginInvoke(App.Dispatcher, () => indicator.IsVisible = false, TimeSpan.FromSeconds(3));
                };
            });
        }
    }
}
