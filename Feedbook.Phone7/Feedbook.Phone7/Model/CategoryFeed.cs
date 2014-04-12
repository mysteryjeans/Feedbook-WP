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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Feedbook.Phone7.Model
{
    public class CategoryFeed : INotifyPropertyChanged
    {
        private string category;

        private bool isPinned;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Category
        {
            get { return this.category; }
            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    this.OnPropertyChanged("Category");
                }
            }
        }

        public bool IsPinned
        {
            get { return this.isPinned; }
            set
            {
                if (this.isPinned != value)
                {
                    this.isPinned = value;
                    this.OnPropertyChanged("IsPinned");
                }
            }
        }

        public ObservableCollection<Feed> Feeds { get; private set; }

        public CategoryFeed()
        {
            this.Feeds = new ObservableCollection<Feed>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }     
}
