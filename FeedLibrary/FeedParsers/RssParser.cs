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
    <?xml version="1.0" encoding="UTF-8"?>
    <rss version="2.0">
        <channel>
            <title>Ukraine news</title>
            <link>http://www.liga.net</link>
            <description>Hot news from Ukraine</description>
            <lastBuildDate>Fri, 29 Dec 2017 00:20:59 +0200</lastBuildDate>
            <ttl>5</ttl>
            <image>
	            <title>Ukrainian news 24/7</title>
	            <url>http://news.liga.net/upload/iblock/e1f/e1ff5a0e53075fb2face32b7db7554e1.gif</url>
	            <link>http://news.liga.net</link>
	            <width>100</width>
	            <height>24</height>
            </image>
            <item>
	            <title>President of Italy want to disassemble the parliament</title>
	            <link>http://news.liga.net/news/world/14877281-prezident_italii_obyavil_o_rospuske_obeikh_palat_parlamenta.htm</link>
	            <description>Mattarella has already planned a date for new parliament election</description>
		        <enclosure url="http://news.liga.net/upload/iblock/581/58148970f3457e1628589476dcbfc470.jpg" length="1146480" type="image/jpeg"/>
		        <pubDate>Thu, 28 Dec 2017 23:59:28 +0200</pubDate>
            </item>
        </channel>
    </rss>
    */

    internal class RssParser : IFeedParser
    {
        public FeedType FeedType => FeedType.RSS;

        public Feed ParseFeed(XDocument document)
        {
            var nodeElements = document.Root.Descendants()
                .First(i => i.Name.LocalName == "channel").Elements().ToArray();
            
            return new Feed
            {
                Name = nodeElements.First(i => i.Name.LocalName == "title").Value,
                Link = nodeElements.First(i => i.Name.LocalName == "link").Value,
                LastUpdated = ParseDate(nodeElements.First(i => i.Name.LocalName == "lastBuildDate").Value),
                Items = ParseFeedItems(nodeElements)
            };
        }

        private IEnumerable<FeedItem> ParseFeedItems(IEnumerable<XElement> nodeElements)
        {
            try
            {
                var entries = from item in nodeElements.Where(i => i.Name.LocalName == "item")
                    select new FeedItem
                    {
                        Title = item.Elements().First(i => i.Name.LocalName == "title").Value,
                        Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                        Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                        PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                        Media = ParseMedia(item)
                    };
                return entries.ToList();
            }
            catch
            {
                return new List<FeedItem>();
            }
        }

        private string ParseMedia(XElement item)
        {
            var result = string.Empty;

            if (item.Elements().Any(i => i.Name.LocalName == "enclosure"))
            {
                result = item.Elements().First(i => i.Name.LocalName == "enclosure").Attribute("url")?.Value;
            }
            else if (item.Elements().Any(i => i.Name.LocalName == "media:thumbnail"))
            {
                result = item.Elements().First(i => i.Name.LocalName == "media:thumbnail").Attribute("url")?.Value;
            }

            result = result ?? string.Empty;

            return result;
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
