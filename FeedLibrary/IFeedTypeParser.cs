using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FeedLibrary
{
    public interface IFeedTypeParser
    {
        FeedType FeedType { get; }

        bool CanParse(XDocument document);
    }
}
