﻿using System;

namespace FeedApp.Models.FeedModels
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
