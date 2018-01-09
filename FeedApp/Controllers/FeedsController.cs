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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Controllers
{
    [Route("api/feeds")]
    public class FeedsController : MyController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly FeedManager _feedManager;

        private readonly IMapper _mapper;

        public FeedsController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext,
            FeedManager feedManager, IMapper mapper) : base(dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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

        /*
         TODO:  Update - updates FeedLabel name or uri
         TODO:  Remove - removes feed from collection
         TODO:  GetFeed - fetches feed for FeedLabel
         TODO:  GetCollectionFeed - fetches all feeds for given collection
         */

        [HttpPost]
        public async Task<IActionResult> Create(Guid feedCollectionId, string name, string url)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollection = await _dbContext.FeedCollections.FindAsync(feedCollectionId);

            if (feedCollection.User.Id == user.Id)
            {
                if (_feedManager.Get(url) == null)
                {
                    return new BadRequestObjectResult("Could not load the feed!");
                }

                var feed = new FeedLabel
                {
                    FeedCollectionId = feedCollectionId,
                    Name = name,
                    Url = url,
                    DateAdded = DateTime.Now
                };

                _dbContext.FeedLabels.Add(feed);

                return SaveDbChanges();
            }

            return new ForbidResult();
        }
    }
}
