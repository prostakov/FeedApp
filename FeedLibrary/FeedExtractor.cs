using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using FeedLibrary.FeedParsers;
using FeedLibrary.Models;

namespace FeedLibrary
{
    public class FeedExtractor
    {
        private readonly Dictionary<FeedType, IFeedParser> _feedParsers;

        private readonly IEnumerable<IFeedTypeParser> _feedTypeParsers;

        public FeedExtractor(IEnumerable<IFeedParser> feedParsers, IEnumerable<IFeedTypeParser> feedTypeParsers)
        {
            _feedTypeParsers = feedTypeParsers;
            _feedParsers = feedParsers.ToDictionary(x => x.FeedType);
        }

        public Feed Process(XDocument document)
        {
            var feedType = _feedTypeParsers.First(f => f.CanParse(document)).FeedType;

            return _feedParsers[feedType].ParseFeed(document);
        }
    }
}
