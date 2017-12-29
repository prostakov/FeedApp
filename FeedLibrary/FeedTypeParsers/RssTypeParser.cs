using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FeedLibrary.FeedTypeParsers
{
    internal class RssTypeParser : IFeedTypeParser
    {
        public FeedType FeedType => FeedType.RSS; 

        public bool CanParse(XDocument document)
        {
            return document.Root?.Name.LocalName == "rss" && 
                document.Root.Attribute("version") != null && 
                document.Root.Attribute("version")?.Value == "2.0";
        }
    }
}
