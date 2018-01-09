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
    public class AtomParserTests
    {
        // TODO: Tests

        //[Test]
        //public void Tests()
        //{
        //    const string feedObjectString = @"<?xml version=""1.0"" encoding=""utf-8""?>
        //        <feed xmlns=""http://www.w3.org/2005/Atom"">
        //            <title>Example Feed</title>
        //            <link href=""http://example.org/"" />
        //            <updated>2003-12-13T18:30:02Z</updated>
        //            <author>
        //                <name>John Doe</name>
        //            </author>
        //            <id>urn:uuid:60a76c80-d399-11d9-b93C-0003939e0af6</id>
        //            <entry>
        //                <title>Atom-Powered Robots Run Amok</title>
        //                <link href=""http://example.org/2003/12/13/atom03"" />
        //                <id>urn:uuid:1225c695-cfb8-4ebb-aaaa-80da344efa6a</id>
        //                <updated>2003-12-13T22:23:24Z</updated>
        //                <summary>Some text.</summary>
        //            </entry>
        //        </feed>";

        //    var feedObject = new Feed
        //    {
        //        Name = "Example Feed",
        //        Link = "http://example.org/",
        //        LastUpdated = new DateTime(2003, 12, 13, 18, 30, 02),
        //        Items = new List<FeedItem>
        //        {
        //            new FeedItem
        //            {
        //                Title = "Atom-Powered Robots Run Amok",
        //                Content = "Some text.",
        //                Link = "http://example.org/2003/12/13/atom03",
        //                Media = "",
        //                PublishDate = new DateTime(2003, 12, 13, 22, 23, 24)
        //            }   
        //        }    
        //    };

        //    var feedExtractor = GetFeedExtractor();

        //    var parsedObject = feedExtractor.Process(XDocument.Parse(feedObjectString));

        //    Assert.AreEqual(parsedObject, feedObject);
        //}

        //private FeedExtractor GetFeedExtractor()
        //{
        //    var feedTypeParsers = new List<IFeedTypeParser> { new AtomTypeParser() };
        //    var feedParsers = new List<IFeedParser> { new AtomParser() };
        //    return new FeedExtractor(feedTypeParsers, feedParsers);
        //}
    }
}