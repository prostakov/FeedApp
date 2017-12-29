using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FeedLibrary.Models;

namespace FeedLibrary.FeedParsers
{
    /* Sample feed:
     * 
    <?xml version="1.0" encoding="utf-8"?>
    <feed xmlns="http://www.w3.org/2005/Atom">
      <title>Example Feed</title>
      <link href="http://example.org/"/>
      <updated>2003-12-13T18:30:02Z</updated>
      <author>
        <name>John Doe</name>
      </author>
      <id>urn:uuid:60a76c80-d399-11d9-b93C-0003939e0af6</id>
      <entry>
        <title>Atom-Powered Robots Run Amok</title>
        <link href="http://example.org/2003/12/13/atom03"/>
        <id>urn:uuid:1225c695-cfb8-4ebb-aaaa-80da344efa6a</id>
        <updated>2003-12-13T18:30:02Z</updated>
        <summary>Some text.</summary>
      </entry>
    </feed>
    */
    
    internal class AtomParser : IFeedParser
    {
        public FeedType FeedType => FeedType.Atom;

        public Feed ParseFeed(XDocument document)
        {
            return new Feed
            {
                Name = "",
                Link = "",
                LastUpdated = new DateTime(2017, 5, 25),
                Items = ParseFeedItems(document)
            };
        }

        private IEnumerable<FeedItem> ParseFeedItems(XDocument document)
        {
            try
            {
                var entries = from item in document.Root.Descendants()
                        .First(i => i.Name.LocalName == "feed").Elements()
                        .Where(i => i.Name.LocalName == "entry")
                    select new FeedItem
                    {
                        Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                        Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                        Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                        PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                        Media = string.Empty
                    };
                return entries.ToList();
            }
            catch
            {
                return new List<FeedItem>();
            }
        }

        private DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }
    }
}
