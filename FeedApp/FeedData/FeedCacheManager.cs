using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using FeedLibrary;
using FeedLibrary.Models;

namespace FeedApp.FeedData
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
                feed.Items = feed.Items.Union(_cache[feedUrl].Feed.Items);

                _cache.TryAdd(feedUrl, (Feed: feed, TimeAdded: DateTime.Now));
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
