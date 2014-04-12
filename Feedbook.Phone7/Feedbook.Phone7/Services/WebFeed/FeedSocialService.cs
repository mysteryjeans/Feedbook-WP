using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPCore.Util;
using Feedbook.Phone7.Model;
using Feedbook.Phone7.Helper;
using System.Windows.Threading;
using System.Threading;
using WPCore.ValueTypeExtension;
using System.Windows;
using Microsoft.Phone.Net.NetworkInformation;

namespace Feedbook.Phone7.Services.WebFeed
{
    internal class FeedSocialService : SocialService
    {
        private FeedProcessor processor = new FeedProcessor();

        public override FeedProcessor Processor { get { return this.processor; } }

        public override bool IsReplySupported { get { return false; } }

        public override bool IsLikeSupported { get { return false; } }

        public override bool IsShareSupported { get { return false; } }

        public override bool IsFollowSupported { get { return false; } }

        public override ISocialUser GetUserProfile()
        {
            throw new NotImplementedException();
        }

        public override ISocialUser[] GetFriends()
        {
            throw new NotSupportedException();
        }

        public override IEnumerable<Channel> GetFeeds()
        {
            throw new NotSupportedException();
        }

        public override void AsyncUpdate(Channel channel)
        {
            Guard.CheckNull(channel, "GetFeedUpdate(channel)");

            if (!NetworkInterface.GetIsNetworkAvailable())
                return;

            lock (channel)
            {
                if (channel.IsSyncing) return;
                channel.IsSyncing = true;                    
            }

            Dispatcher dispatcher = HelperFunction.GetDispatcher();
            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (object o) =>
                {
                    try
                    {
                        string xmlString = Downloader.DownloadString(channel.DownloadUrl);
                        var updatedChannel = this.processor.Parse(xmlString, channel.DownloadUrl);

                        dispatcher.BeginInvoke(new Action(() =>
                        {
                            foreach (var feed in updatedChannel.Feeds)
                            {
                                var existingFeed = channel.Feeds.FirstOrDefault(f => f.Guid == feed.Guid);
                                if (existingFeed != null)
                                {
                                    if (existingFeed.Updated.GetUnixTime() < feed.Updated.GetUnixTime())
                                    {
                                        channel.Feeds.Add(feed);
                                        channel.Feeds.Remove(existingFeed);
                                        channel.NewFeedCount += 1;
                                    }
                                }
                                else
                                {
                                    channel.Feeds.Add(feed);
                                    channel.NewFeedCount += 1;
                                }
                            }

                            foreach (var feed in channel.Feeds.OrderByDescending(f => f.Updated)
                                                              .Skip(SysConfig.MaxWebFeed)
                                                              .ToArray())
                                channel.Feeds.Remove(feed);

                            channel.Updated = updatedChannel.Updated;
                        }));

                    }
                    catch (Exception ex)
                    {
                        HelperFunction.HandleBackgroundException("Syncing failed...", ex);
                    }
                    finally
                    {
                        dispatcher.BeginInvoke(new Action(() =>
                        {
                            lock (channel)
                            {
                                channel.IsSyncing = false;
                            }
                        }));
                    }
                }));

        }

        public override Feed Post(string message, Attachment[] attachments)
        {
            throw new NotSupportedException();
        }

        public override Feed Update(Feed feed, string message, Attachment[] attachments)
        {
            throw new NotSupportedException();
        }

        public override Feed Reply(Feed feed, string comments)
        {
            throw new NotSupportedException();
        }

        public override void Like(Feed feed)
        {
            throw new NotSupportedException();
        }

        public override void Remove(Feed feed)
        {
            throw new NotSupportedException();
        }

        public override void Share(Feed feed)
        {
            throw new NotSupportedException();
        }

        public override void Follow(Person person)
        {
            throw new NotSupportedException();
        }

        public override void UnFollow(Person person)
        {
            throw new NotSupportedException();
        }
    }
}
