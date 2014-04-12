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
using System.IO;
using System.Threading;

namespace Feedbook.Phone7.Helper
{
    public static class HttpWebRequestExtension
    {
        public static Stream GetRequestStream(this HttpWebRequest request)
        {
            var result = request.BeginGetRequestStream(delegate { }, null);
            while (!result.IsCompleted) Thread.Sleep(100);
            return request.EndGetRequestStream(result);
        }

        public static WebResponse GetResponse(this HttpWebRequest request)
        {
            var result = request.BeginGetResponse(delegate { }, null);
            while (!result.IsCompleted) Thread.Sleep(100);
            return request.EndGetResponse(result);
        }
    }
}
