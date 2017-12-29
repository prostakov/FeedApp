using System;
using System.Collections.Generic;
using System.Text;

namespace FeedLibrary.Models
{
    public class Feed
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public DateTime LastUpdated { get; set; }

        public IEnumerable<FeedItem> Items { get; set; }
    }
}
