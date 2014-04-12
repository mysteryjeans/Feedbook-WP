using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Feedbook.Phone7.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WPCore.RefTypeExtension;
using Feedbook.Phone7.Services;
using Feedbook.Phone7.Helper;
using Microsoft.Phone.Tasks;
using System.Windows.Data;
using Feedbook.Phone7.Automation;
using System.Collections.Specialized;
using System.Windows.Controls.Primitives;
using System.Threading;

namespace Feedbook.Phone7.Views
{
    public partial class Tags : UserControl
    {
        public Tags()
        {
            InitializeComponent();

            Storage.Favorites.CollectionChanged += (sender, e) =>
            {
                if (Storage.Favorites.Count == 0)
                {
                    this.CategoryViewSource.Source = new CategoryFeed[] { new CategoryFeed { Category = "all" } };
                    this.FavoritesListBox.ItemsSource = this.CategoryViewSource.View;
                }
                else if (this.CategoryViewSource.Source != Storage.Favorites)
                {
                    this.BeginInvoke(() =>
                    {
                        this.CategoryViewSource.Source = Storage.Favorites;
                        this.FavoritesListBox.ItemsSource = this.CategoryViewSource.View;
                    });
                }
            };

            this.LoadFeeds();
            this.CategoryViewSource.Source = Storage.Favorites;
            this.FavoritesListBox.ItemsSource = this.CategoryViewSource.View;
            Storage.Channels.CollectionChanged += (sender, e) => this.LoadFeeds();
            GlobalEventManager.OnFeedAdd += new FeedEventHandler(OnFeedAdd);
        }

        private void OnFeedAdd(object sender, FeedEventArgs e)
        {
            if (e.Source == FeedSource.RSS)
            {
                foreach (var category in e.Feed.Categories)
                {
                    var categoryFeed = Storage.Favorites.FirstOrDefault(c => string.Equals(category.Name, c.Category, StringComparison.OrdinalIgnoreCase));
                    if (categoryFeed != null)
                        categoryFeed.Feeds.Add(e.Feed);
                }
            }
        }

        private void LoadFeeds()
        {
            foreach (var categoryFeed in Storage.Favorites)
            {
                categoryFeed.Feeds.Clear();
                categoryFeed.Feeds.AddRange((from channel in Storage.Channels
                                             from feed in channel.Feeds
                                             from category in feed.Categories
                                             where string.Equals(category.Name, categoryFeed.Category, StringComparison.OrdinalIgnoreCase)
                                             select feed).Distinct());
            }
        }
    }
}