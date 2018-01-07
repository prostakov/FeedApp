using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using FeedLibrary;

namespace FeedApp.Data
{
    public class FeedManager
    {
        private readonly FeedExtractor _feedExtractor;

        private readonly ConcurrentDictionary<string, (FeedLibrary.Models.Feed Feed, DateTime TimeAdded)> _cache =
            new ConcurrentDictionary<string, (FeedLibrary.Models.Feed Feed, DateTime TimeAdded)>();

        private const int CacheExpirationPeriodMinutes = 20;

        public FeedManager(FeedExtractor feedExtractor)
        {
            _feedExtractor = feedExtractor ?? throw new ArgumentNullException(nameof(feedExtractor));
        }

        public IDictionary<string, FeedLibrary.Models.Feed> Get(string[] feedUrls)
        {
            //return feedUrls.AsParallel()
            //    .Select(feedUrl => new { url = feedUrl, feed = Get(feedUrl) })
            //    .Where(x => x.feed != null)
            //    .ToDictionary(x => x.url, x => x.feed);

            var feeds = new ConcurrentDictionary<string, FeedLibrary.Models.Feed>();

            Parallel.ForEach(feedUrls, feedUrl =>
            {
                var feed = Get(feedUrl);

                if (feed != null)
                {
                    feeds.TryAdd(feedUrl, feed);
                }
            });

            return feeds;
        }

        private FeedLibrary.Models.Feed Get(string feedUrl)
        {
            return _cache.ContainsKey(feedUrl) && !CacheItemExpired(feedUrl)
                ? GetFromCache(feedUrl)
                : LoadAndCache(feedUrl);
        }

        private bool CacheItemExpired(string feedUrl)
        {
            return _cache.TryGetValue(feedUrl, out var feedTuple) &&
                   (DateTime.Now - feedTuple.TimeAdded).Minutes > CacheExpirationPeriodMinutes;
        }

        private FeedLibrary.Models.Feed GetFromCache(string feedUrl)
        {
            return _cache.TryGetValue(feedUrl, out var value) ? value.Feed : null;
        }

        private FeedLibrary.Models.Feed LoadAndCache(string feedUrl)
        {
            var feed = LoadFeed(feedUrl);

            if (feed != null)
            {
                var newTuple = (Feed: feed, TimeAdded: DateTime.Now);
                _cache.AddOrUpdate(feedUrl, newTuple, (s, oldTuple) => newTuple);
            }

            return feed;
        }

        private FeedLibrary.Models.Feed LoadFeed(string feedUrl)
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
