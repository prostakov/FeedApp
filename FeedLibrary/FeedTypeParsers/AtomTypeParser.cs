using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FeedLibrary.FeedTypeParsers
{
    internal class AtomTypeParser : IFeedTypeParser
    {
        public FeedType FeedType => FeedType.Atom;

        public bool CanParse(XDocument document)
        {
            return document.Root?.Name.LocalName == "feed" && 
                document.Root.Attribute("xmlns") != null && 
                document.Root.Attribute("xmlns")?.Value == "http://www.w3.org/2005/Atom";
        }
    }
}
