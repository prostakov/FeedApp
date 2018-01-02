using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using FeedLibrary;
using FeedLibrary.Models;

namespace FeedApp.Data
{
    public class FeedCacheManager
    {
        private readonly FeedExtractor _feedExtractor;

        private readonly ConcurrentDictionary<string, (Feed Feed, DateTime TimeAdded)> _cache =
            new ConcurrentDictionary<string, (Feed Feed, DateTime TimeAdded)>();

        private readonly ConcurrentQueue<string> _lazyUpdateQueue = new ConcurrentQueue<string>();

        private const int CacheExpirationPeriodMinutes = 5;

        public FeedCacheManager(FeedExtractor feedExtractor)
        {
            _feedExtractor = feedExtractor ?? throw new ArgumentNullException(nameof(feedExtractor));
        }

        public bool CacheContains(string feedUrl)
        {
            return _cache.ContainsKey(feedUrl);
        }

        public void ScheduleLazyUpdate(string feedUrl)
        {
            if (!_lazyUpdateQueue.Contains(feedUrl))
            {
                _lazyUpdateQueue.Enqueue(feedUrl);
            }
        }

        public Feed GetFromCache(string feedUrl)
        {
            return _cache.TryGetValue(feedUrl, out var value) ? value.Feed : null;
        }

        public Feed LoadAndCache(string feedUrl)
        {
            var feed = LoadFeed(feedUrl);

            if (feed != null)
            {
                _cache.TryAdd(feedUrl, (Feed: feed, TimeAdded: DateTime.Now));
            }

            return feed;
        }

        public void LazyUpdate()
        {
            while (true)
            {
                if (_lazyUpdateQueue.Count > 0)
                {
                    if (_lazyUpdateQueue.TryDequeue(out string feedUrl) && CacheItemExpired(feedUrl))
                    {
                        UpdateCacheItem(feedUrl);
                    }
                }
            }
        }

        private bool CacheItemExpired(string feedUrl)
        {
            return _cache.TryGetValue(feedUrl, out var feedTuple) && 
                (DateTime.Now - feedTuple.TimeAdded).Minutes > CacheExpirationPeriodMinutes;
        }

        private void UpdateCacheItem(string feedUrl)
        {
            var feed = LoadFeed(feedUrl);

            if (feed != null)
            {
                if (_cache.TryGetValue(feedUrl, out var cachedFeedTuple) && cachedFeedTuple.Feed.Items.Any())
                {
                    feed.Items = feed.Items.Union(cachedFeedTuple.Feed.Items);
                }

                var newFeedTuple = (Feed: feed, TimeAdded: DateTime.Now);

                _cache.AddOrUpdate(feedUrl, newFeedTuple, (key, oldFeedTuple) => newFeedTuple);
            }
        }

        private Feed LoadFeed(string feedUrl)
        {
            try
            {
                var document = XDocument.Load(feedUrl);

                return _feedExtractor.Process(document);
            }
            catch
            {
                return null;
            }
        }
    }
}
