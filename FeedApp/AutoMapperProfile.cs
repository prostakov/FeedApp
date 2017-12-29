using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FeedApp.Models;

namespace FeedApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FeedLibrary.Models.Feed, Feed>();
            CreateMap<FeedLibrary.Models.FeedItem, FeedItem>();
        }
    }
}
