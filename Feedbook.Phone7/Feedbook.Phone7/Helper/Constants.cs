using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace Feedbook.Phone7
{
    internal static class Constants
    {
        public const string APPLICATION_NAME = "Feedbook";

        public const string WEBSITE_URL = "http://www.feedbook.org";

        public const string SUPPORT_EMAIL = "info@feedbook.org";

        public const string ICON_URL = "https://lh6.googleusercontent.com/_o-iIggNHfDU/TD8kde_JezI/AAAAAAAAAIY/VyOvUy8cWIw/Feedbook%20Logo-256x256.png";

        public const string XML_TAG_REGEX = @"</?\w+\s+[^>]*>";

        public const string ANCHOR_LINK_TAG_REGEX = @"<(?<tag>a|link)[^>]*>";

        public const string IMAGE_TAG_REGEX = @"<(?<tag>img)[^>]*>";
        
        public const string HREF_ATTRIBUTE_REGEX = "href\\s*=\\s*(?:\"(?<url>[^\"]*)\"|(?<url>\\S+))";

        public const string SRC_ATTRIBUTE_REGEX = "src\\s*=\\s*(?:\"(?<src>[^\"]*)\"|(?<src>\\S+))";

        public const string TYPE_ATTRIBUTE_REGEX = "type\\s*=\\s*(?:\"(?<type>[^\"]*)\"|(?<type>\\S+))";

        public const string RESX_RSS_IMAGE_URI = "/Feedbook.Phone7;component/Images/RSS.png";

        public const string RESX_TWITTER_IMAGE_URI = "/Feedbook.Phone7;component/Images/Twitter.png";

        public const string RESX_GOOGLE_BUZZ_IMAGE_URI = "/Feedbook.Phone7;component/Images/Buzz.png";

        public const string RESX_AUDIO_IMAGE_URI = "/Feedbook.Phone7;component/Images/audio.png";

        public const string RESX_VIDEO_IMAGE_URI = "/Feedbook.Phone7;component/Images/video.png";

        public const string RESX_ATTACHMENT_IMAGE_URI = "/Feedbook.Phone7;component/Images/attachment.png";

        public static readonly DateTime UnixReferenceDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static class Default
        {
            public const int ShowMilliseconds = 300;

            public const int HideMilliseconds = 400;

            public const int PopupDurationMilliseconds = 3000;
        }

        public static class Caption
        {
            public const string APP_NAME = "Feedbook";

            public const string INFO = "Information";

            public const string ERROR = "Error";

            public const string EXCEPTION = "Exception";

            public const string WARNING = "Warning";
        }

        public static class Page
        {
            public static readonly Uri MAIN_PAGE = new Uri("/Views/MainPage.xaml", UriKind.Relative);

            public static readonly Uri FEED_PAGE = new Uri("/Views/FeedPage.xaml", UriKind.Relative);

            public static readonly Uri IMAGE_VIEWER = new Uri("/Views/ImageViewerPage.xaml", UriKind.Relative);

            public static readonly Uri FLASH_VIEWER = new Uri("/Views/FlashViewerPage.xaml", UriKind.Relative);

            public static readonly Uri FEED_DISCOVERY = new Uri("/Views/DiscoveryPage.xaml", UriKind.Relative);

            public static readonly Uri SUBSCRIPTIONS_PAGE = new Uri("/Views/SubscriptionsPage.xaml", UriKind.Relative);

            public static readonly Uri TAGS_PAGE = new Uri("/Views/TagsPage.xaml", UriKind.Relative);

            public static readonly Uri PANORAMA_FEED_PAGE = new Uri("/Views/PanoramaFeedPage.xaml", UriKind.Relative);

            public static readonly Uri SETTINGS = new Uri("/Views/SettingsPages.xaml", UriKind.Relative);

            public static readonly Uri ABOUT = new Uri("/Views/AboutPage.xaml", UriKind.Relative);
        }

        public static string BytesToHex(byte[] bytes)
        {
            string hexStr = null;
            foreach (byte b in bytes)
                hexStr += b.ToString("x2");

            return hexStr;
        }        
    }
}
