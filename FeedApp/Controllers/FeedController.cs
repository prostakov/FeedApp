using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using FeedApp.Models;
using FeedLibrary;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Controllers
{
    [Route("api/feed")]
    public class FeedController : Controller
    {
        private const string _sourceLigaNet = "http://news.liga.net/all/rss.xml";
        private const string _sourceWired = "https://www.wired.com/feed/rss";

        private readonly FeedExtractor _feedExtractor;

        private readonly IMapper _mapper;

        public FeedController(FeedExtractor feedExtractor, IMapper mapper)
        {
            _feedExtractor = feedExtractor ?? throw new ArgumentNullException(nameof(feedExtractor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/values
        [HttpGet]
        public Feed Get()
        {
            var feed = _feedExtractor.Get(_sourceWired);
            return _mapper.Map<FeedLibrary.Models.Feed, Feed>(feed);
        }
    }
}
