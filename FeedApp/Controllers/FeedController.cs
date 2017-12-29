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

        private readonly FeedManager _feedManager;

        public FeedController(FeedManager feedManager)
        {
            _feedManager = feedManager ?? throw new ArgumentNullException(nameof(feedManager));
        }

        // GET api/values
        [HttpGet]
        public async Task<Feed> Get()
        {
            return await _feedManager.GetFeed(_sourceWired);
        }
    }
}
