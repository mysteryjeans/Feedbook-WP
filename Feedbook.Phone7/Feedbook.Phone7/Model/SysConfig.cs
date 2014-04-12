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

namespace Feedbook.Phone7.Model
{
    public class SysConfig
    {
        public static bool PanoramaFeedView
        {
            get
            {
                var panoramaFeedView = Storage.GetObject<bool?>("PANORAMA_FEED_VIEW");
                if (panoramaFeedView == default(bool?))
                    SysConfig.PanoramaFeedView = (panoramaFeedView = true).GetValueOrDefault();

                return panoramaFeedView.Value;
            }
            set { Storage.SaveObject<bool?>("PANORAMA_FEED_VIEW", value); }
        }

        public static int MaxWebFeed
        {
            get
            {
                var maxWebFeed = Storage.GetObject<int>("MAX_WEB_FEED");
                if (maxWebFeed == default(int))
                    SysConfig.MaxWebFeed = maxWebFeed = 20;

                return maxWebFeed;
            }
            set { Storage.SaveObject<int>("MAX_WEB_FEED", value); }
        }

        public static TimeSpan SynchronizeInterval
        {
            get
            {
                var synchronizeInterval = Storage.GetObject<TimeSpan>("SYNCHRONIZE_INTERVAL");
                if (synchronizeInterval == default(TimeSpan))
                   SysConfig.SynchronizeInterval = synchronizeInterval = TimeSpan.FromMinutes(3);

                return synchronizeInterval;
            }
            set { Storage.SaveObject<TimeSpan>("SYNCHRONIZE_INTERVAL", value); }
        }

        public static int MaxPostLength
        {
            get { return 420; }
        }
    }
}
