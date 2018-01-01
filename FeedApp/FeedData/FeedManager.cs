using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using FeedLibrary;
using FeedLibrary.Models;

namespace FeedApp.FeedData
{
    public class FeedManager
    {
        private readonly FeedCacheManager _cacheManager;

        public FeedManager(FeedCacheManager cacheManager)
        {
            _cacheManager = cacheManager ?? throw new ArgumentNullException(nameof(cacheManager));
            Task.Factory.StartNew(_cacheManager.LazyUpdate);
        }

        public ConcurrentDictionary<string, Feed> Get(string[] feedUrls)
        {
            var feeds = new ConcurrentDictionary<string, Feed>();

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

        private Feed Get(string feedUrl)
        {
            if (_cacheManager.CacheContains(feedUrl))
            {
                _cacheManager.ScheduleLazyUpdate(feedUrl);

                return _cacheManager.GetFromCache(feedUrl);
            }
            else
            {
                return _cacheManager.LoadAndCache(feedUrl);
            }
        }
    }
}
