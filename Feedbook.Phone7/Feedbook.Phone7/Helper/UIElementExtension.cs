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

namespace Feedbook.Phone7.Helper
{
    public static class UIElementExtension
    {
        public static bool IsVisible(this UIElement element)
        {
            return element.Visibility == Visibility.Visible;
        }

        public static void Show(this UIElement element, int milliseconds)
        {
            if (!element.IsVisible())
            {
                element.Show();
                element.RenderTransformOrigin = new Point(0, 0);
                element.RenderTransform = new CompositeTransform { ScaleX = 0, ScaleY = 0 };

                Duration duration = new Duration(TimeSpan.FromMilliseconds(milliseconds));

                DoubleAnimation scaleX = new DoubleAnimation { To = 1, Duration = duration };
                scaleX.EasingFunction = new BackEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut };
                Storyboard.SetTargetProperty(scaleX, new PropertyPath("ScaleX"));

                DoubleAnimation scaleY = new DoubleAnimation { To = 1, Duration = duration };
                scaleY.EasingFunction = new BackEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut };
                Storyboard.SetTargetProperty(scaleY, new PropertyPath("ScaleY"));

                Storyboard.SetTarget(scaleX, element.RenderTransform);
                Storyboard.SetTarget(scaleY, element.RenderTransform);

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(scaleX);
                storyboard.Children.Add(scaleY);

                storyboard.Begin();
            }
        }

        public static void Hide(this UIElement element, int milliseconds)
        {
            if (element.IsVisible())
            {
                element.RenderTransformOrigin = new Point(0, 0);
                element.RenderTransform = new CompositeTransform { ScaleX = 1, ScaleY = 1 };

                Duration duration = new Duration(TimeSpan.FromMilliseconds(milliseconds));

                DoubleAnimation scaleX = new DoubleAnimation { To = 0, Duration = duration };
                Storyboard.SetTargetProperty(scaleX, new PropertyPath("ScaleX"));

                DoubleAnimation scaleY = new DoubleAnimation { To = 0, Duration = duration };
                Storyboard.SetTargetProperty(scaleY, new PropertyPath("ScaleY"));

                Storyboard.SetTarget(scaleX, element.RenderTransform);
                Storyboard.SetTarget(scaleY, element.RenderTransform);

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(scaleX);
                storyboard.Children.Add(scaleY);

                storyboard.Begin();

                element.BeginInvoke(() => element.Hide(), milliseconds);
            }
        }

        public static void Show(this UIElement element)
        {
            element.Visibility = Visibility.Visible;
        }

        public static void Hide(this UIElement element)
        {
            element.Visibility = Visibility.Collapsed;
        }

        public static void BeginInvoke(this UIElement element, Action action)
        {
            element.BeginInvoke(action, TimeSpan.Zero);
        }

        public static void BeginInvoke(this UIElement element, Action action, TimeSpan wait)
        {
            HelperFunction.BeginInvoke(element.Dispatcher, action, wait);
        }

        public static void BeginInvoke(this UIElement element, Action action, int milliseconds)
        {
            HelperFunction.BeginInvoke(element.Dispatcher, action, TimeSpan.FromMilliseconds(milliseconds));
        }
    }
}
