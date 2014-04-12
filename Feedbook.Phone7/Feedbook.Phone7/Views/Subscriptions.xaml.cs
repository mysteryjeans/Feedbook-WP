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
using System.Windows.Controls.Primitives;

namespace Feedbook.Phone7.Views
{
    public partial class Subscriptions : UserControl
    {
        private ObservableCollection<Channel> channels = new ObservableCollection<Channel>();
        private Channel all = new Channel { ChannelId = "-1", Title = "all feeds" };

        public Subscriptions()
        {
            InitializeComponent();

            this.channels.Add(all);
            this.channels.AddRange(Storage.Channels);

            var feeds = (from channel in Storage.Channels
                         from feed in channel.Feeds
                         orderby feed.Updated descending
                         select feed).Take(SysConfig.MaxWebFeed);
            this.all.Feeds.AddRange(feeds);

            GlobalEventManager.OnFeedAdd += (sender, e) => this.all.Feeds.Add(e.Feed);
            GlobalEventManager.OnFeedRemove += (sender, e) => this.all.Feeds.Remove(e.Feed);

            Storage.Channels.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Channels_CollectionChanged);

            this.SubscriptionsListBox.ItemsSource = this.channels;
            this.FeedListBox.ItemsSource = this.FeedViewSource.View;
        }

        void Channels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Channel channel in e.NewItems)
                    this.channels.Add(channel);

            if (e.OldItems != null)
                foreach (Channel channel in e.OldItems)
                    this.channels.Remove(channel);

            this.all.Feeds.OrderByDescending(f => f.Updated)
                          .Skip(SysConfig.MaxWebFeed)
                          .ToArray()
                          .ForEach(f => this.all.Feeds.Remove(f));
        }

        private void RemoveChannelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null)
            {
                var popup = button.GetVisualAncestors()
                                  .OfType<Popup>()
                                  .FirstOrDefault();

                if (popup != null)
                    popup.IsOpen = false;
            }

            var channel = HelperFunction.DataContextAs<Channel>(e.OriginalSource);
            if (channel != null)
            {
                Storage.Channels.Remove(channel);
                this.SubscriptionsListBox.SelectedItem = Storage.Channels.FirstOrDefault();
            }
        }

        private void SubscriptionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var channel = this.SubscriptionsListBox.SelectedItem as Channel;
                if (channel != null)
                {
                    this.FeedViewSource.Source = channel.Feeds;
                    this.FeedListBox.ItemsSource = this.FeedViewSource.View;
                }
            }
            catch (Exception ex)
            {
                HelperFunction.HandleException(ex);
            }
        }
    }
}