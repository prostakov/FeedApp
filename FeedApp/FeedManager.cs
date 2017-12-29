using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using FeedApp.Models;
using FeedLibrary;

namespace FeedApp
{
    public class FeedManager
    {
        private readonly FeedExtractor _feedExtractor;

        private readonly IMapper _mapper;

        private readonly Dictionary<string, Feed> _feedCache;

        private readonly object _cacheLock = new object();

        public FeedManager(FeedExtractor feedExtractor, IMapper mapper)
        {
            _feedExtractor = feedExtractor ?? throw new ArgumentNullException(nameof(feedExtractor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _feedCache = new Dictionary<string, Feed>();
        }

        public async Task<Feed> GetFeed(string feedUrl)
        {
            if (_feedCache.ContainsKey(feedUrl))
            {
                UpdateFeedInCache(feedUrl);

                return _feedCache[feedUrl];
            }
            else
            {
                var feed = await UpdateFeedInCache(feedUrl);

                return feed;
            }
        }

        private async Task<Feed> UpdateFeedInCache(string feedUri)
        {
            // TODO: implement more sophisticated logic: when for example one new feed entry is added, and we need to just update the feed item, not replace it

            lock (_cacheLock)
            {
                var feed = LoadFeed(feedUri);

                _feedCache[feedUri] = feed;

                return feed;
            }
        }

        private Feed LoadFeed(string feedUrl)
        {
            var document = XDocument.Load(feedUrl);

            var feed = _feedExtractor.Process(document);

            return _mapper.Map<FeedLibrary.Models.Feed, Feed>(feed);
        }
    }
}
