using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using FeedLibrary.Models;

namespace FeedLibrary.FeedParsers
{
    public interface IFeedParser
    {
        FeedType FeedType { get; }

        Feed ParseFeed(XDocument document);
    }
}
