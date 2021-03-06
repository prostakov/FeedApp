﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedApp.Models.ResourceModels
{
    public class FeedLabelResource
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
