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
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using WPCore.Util;

namespace WPCore.ObjectModel
{
    public class QueryCollection<T> : ObservableCollection<T>
    {
        private IEnumerable<T> query;

        public QueryCollection(IEnumerable<T> query, INotifyCollectionChanged source)
        {
            Guard.CheckNull(query, "Query cannot be null");
            Guard.CheckNull(source, "Source cannot be null");

            this.query = query;
            source.CollectionChanged +=new NotifyCollectionChangedEventHandler(source_CollectionChanged);
        }

        void source_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var result = query.ToArray();

            foreach (var item in this.ToArray())
                if (!result.Contains(item))
                    this.Remove(item);

            foreach (var item in result)
                if (!this.Contains(item))
                    this.Add(item);
        }       
    }
}
