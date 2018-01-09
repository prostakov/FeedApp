using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using FeedApp.Data;
using FeedApp.Models;
using FeedLibrary;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Controllers
{
    [Route("api/feed")]
    public class FeedsController : Controller
    {
        private readonly FeedManager _feedManager;

        private readonly IMapper _mapper;

        public FeedsController(FeedManager feedManager, IMapper mapper)
        {
            _feedManager = feedManager ?? throw new ArgumentNullException(nameof(feedManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Feed> Get()
        {
            var sources = System.IO.File.ReadAllLines("rss_test_feeds.txt");

            var feeds = _feedManager.Get(sources);

            return _mapper.Map<IEnumerable<FeedLibrary.Models.Feed>, IEnumerable<Feed>>(feeds.Values);
        }
    }
}
