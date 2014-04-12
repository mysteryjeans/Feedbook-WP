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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using WPCore.Util;

namespace WPCore.RefTypeExtension
{
    public static class CollectionExtension
    {
        public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> enumrable)
        {
            Guard.CheckNull(enumrable, "AddRange(enumerable");
            enumrable.ForEach(item => collection.Add(item));
        }
    }
}
