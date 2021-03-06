﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Remotion.Linq.Parsing.ExpressionVisitors.MemberBindings;

namespace FeedApp.Models.ResourceModels
{
    public class FeedCollectionResource
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateAdded { get; set; }

        public ICollection<FeedLabelResource> Feeds { get; set; }
    }
}
