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
using System.Windows.Threading;
using WPCore.Util;

namespace Feedbook.Phone7.Automation
{
    public enum SyncBar
    {
        Both,
        Vertical,
        Horizontal
    }

    public class ScrollSynchronizer
    {
        private ScrollViewer source;
        private ScrollViewer[] targets;
        private DispatcherTimer timer = new DispatcherTimer();

        public ScrollSynchronizer(ScrollViewer source, ScrollViewer[] targets, SyncBar syncBar)
        {
            Guard.CheckNull(source, "Source scroll cannot be empty");
            Guard.CheckNull(targets, "Targes scroll cannot be empty");

            this.source = source;
            this.targets = targets;

            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            switch (syncBar)
            {
                case SyncBar.Vertical:
                    this.timer.Tick += delegate(object sender, EventArgs e)
                    {
                        foreach (var target in this.targets)
                            this.VerticalSync(target);
                    };
                    break;
                case SyncBar.Horizontal:
                    this.timer.Tick += delegate(object sender, EventArgs e)
                    {
                        foreach (var target in this.targets)
                            this.HorizontalSync(target);
                    };
                    break;
                default:
                    this.timer.Tick += delegate(object sender, EventArgs e)
                    {
                        foreach (var target in this.targets)
                        {
                            this.HorizontalSync(target);
                            this.VerticalSync(target);
                        }
                    };
                    break;
            }
        }

        public void Start()
        {
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        private void HorizontalSync(ScrollViewer target)
        {
            var currentoffset = target.HorizontalOffset;

            var changed = target.HorizontalOffset - this.source.HorizontalOffset;

            var acceleration = (target.ViewportWidth - changed) / target.ViewportWidth;

            changed = (changed * acceleration * 0.03);

            if (this.source.HorizontalOffset < target.HorizontalOffset && changed > 0)
                changed *= -1;
            else if (this.source.HorizontalOffset > target.HorizontalOffset && changed < 0)
                changed *= -1;

            target.ScrollToHorizontalOffset(currentoffset + changed);
        }

        private void VerticalSync(ScrollViewer target)
        {
            var currentoffset = target.VerticalOffset;

            var changed = target.VerticalOffset - this.source.VerticalOffset;

            var acceleration = (target.ViewportHeight - changed) / target.ViewportHeight;

            changed = (changed * acceleration * 0.03);

            if (this.source.VerticalOffset < target.VerticalOffset && changed > 0)
                changed *= -1;
            else if (this.source.VerticalOffset > target.VerticalOffset && changed < 0)
                changed *= -1;

            target.ScrollToVerticalOffset(currentoffset + changed);
        }
    }
}
