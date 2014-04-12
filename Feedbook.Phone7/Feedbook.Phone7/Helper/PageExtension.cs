using System;
using System.Net;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Feedbook.Phone7.Helper
{
    public static class PageExtension
    {
        private static readonly Dictionary<string, object> session = new Dictionary<string, object>();

        public static void SetSession(this Page page, string key, object value)
        {
            session[key] = value;
        }

        public static object GetSession(this Page page, string key)
        {
            if (session.ContainsKey(key))
                return session[key];

            return null;
        }
    }
}
