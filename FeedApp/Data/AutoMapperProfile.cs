using AutoMapper;
using FeedApp.Models;
using FeedApp.Models.ResourceModels;

namespace FeedApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FeedLibrary.Models.Feed, Feed>();
            CreateMap<FeedLibrary.Models.FeedItem, FeedItem>();

            CreateMap<FeedCollection, FeedCollectionResource>();
        }
    }
}
