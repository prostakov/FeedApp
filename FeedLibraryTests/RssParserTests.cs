using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using FeedLibrary;
using FeedLibrary.FeedParsers;
using FeedLibrary.FeedTypeParsers;
using FeedLibrary.Models;
using NUnit.Framework;

namespace FeedLibraryTests
{
    [TestFixture]
    public class RssParserTests
    {
        // TODO: Tests

        //[Test]
        //public void Tests()
        //{
        //    const string feedObjectString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
        //        <rss version=""2.0"">
        //            <channel>
        //                <title>Ukraine news</title>
        //                <link>http://www.liga.net</link>
        //                <description>Hot news from Ukraine</description>
        //                <lastBuildDate>Fri, 29 Dec 2017 00:20:59 +0200</lastBuildDate>
        //                <ttl>5</ttl>
        //                <image>
        //                    <title>Ukrainian news 24/7</title>
        //                    <url>http://news.liga.net/upload/iblock/e1f/e1ff5a0e53075fb2face32b7db7554e1.gif</url>
        //                    <link>http://news.liga.net</link>
        //                    <width>100</width>
        //                    <height>24</height>
        //                </image>
        //                <item>
        //                    <title>President of Italy want to disassemble the parliament</title>
        //                    <link>http://news.liga.net/news/world/14877281-prezident_italii_obyavil_o_rospuske_obeikh_palat_parlamenta.htm</link>
        //                    <description>Mattarella has already planned a date for new parliament election</description>
        //                    <enclosure url=""http://news.liga.net/upload/iblock/581/58148970f3457e1628589476dcbfc470.jpg"" length=""1146480"" type=""image/jpeg"" />
        //                    <pubDate>Thu, 28 Dec 2017 23:59:28 +0200</pubDate>
        //                </item>
        //            </channel>
        //        </rss>";

        //    var feedObject = new Feed
        //    {
        //        Name = "Ukraine news",
        //        Link = "http://www.liga.net",
        //        LastUpdated = new DateTime(2017, 12, 29, 0, 20, 59),
        //        Items = new List<FeedItem>
        //        {
        //            new FeedItem
        //            {
        //                Title = "President of Italy want to disassemble the parliament",
        //                Content = "Mattarella has already planned a date for new parliament election",
        //                Link =
        //                    "http://news.liga.net/news/world/14877281-prezident_italii_obyavil_o_rospuske_obeikh_palat_parlamenta.htm",
        //                Media = "http://news.liga.net/upload/iblock/581/58148970f3457e1628589476dcbfc470.jpg",
        //                PublishDate = new DateTime(2017, 12, 28, 23, 59, 28)
        //            }
        //        }
        //    };

        //    var feedExtractor = GetFeedExtractor();

        //    var parsedObject = feedExtractor.Process(XDocument.Parse(feedObjectString));

        //    Assert.AreEqual(parsedObject, feedObject);
        //}

        //private FeedExtractor GetFeedExtractor()
        //{
        //    var feedTypeParsers = new List<IFeedTypeParser> { new RssTypeParser() };
        //    var feedParsers = new List<IFeedParser> { new RssParser() };
        //    return new FeedExtractor(feedTypeParsers, feedParsers);
        //}
    }
}
