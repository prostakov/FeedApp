using System;
using System.Collections.Generic;
using System.Text;

namespace FeedLibrary.Models
{
    public class FeedItem
    {
        public string Link { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Media { get; set; }

        public DateTime PublishDate { get; set; }

        public FeedItem()
        {
            Link = "";
            Title = "";
            Content = "";
            PublishDate = DateTime.Today;
        }
    }
}
