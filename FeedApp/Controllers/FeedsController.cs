﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using FeedApp.Data;
using FeedApp.Models;
using FeedApp.Models.RequestModels;
using FeedLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        /*
         TODO:  Update - updates FeedLabel name or uri
         TODO:  Remove - removes feed from collection
         TODO:  GetFeed - fetches feed for FeedLabel
         TODO:  GetCollectionFeed - fetches all feeds for given collection
         */

        [HttpPost]
        [ValidateRequest]
        public async Task<IActionResult> Create(CreateFeedLabelRequest request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollection = await _dbContext.FeedCollections.FindAsync(request.FeedCollectionId);

            if (feedCollection.User.Id == user.Id)
            {
                if (_feedManager.Get(request.Url) == null)
                {
                    return new BadRequestObjectResult("Could not load the feed!");
                }

                var feed = new FeedLabel
                {
                    FeedCollectionId = request.FeedCollectionId,
                    Name = request.Name,
                    Url = request.Url,
                    DateAdded = DateTime.Now
                };

                _dbContext.FeedLabels.Add(feed);

                return SaveDbChanges();
            }

            return Forbid();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid feedLabelId, string name, string url)
        {
            var feed = _dbContext.FeedLabels
                .Include(f => f.FeedCollection)
                .FirstOrDefault(f => f.Id == feedLabelId);

            if (feed != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (feed.FeedCollection.UserId == user.Id)
                {
                    
                }

                return Forbid();
            }

            return BadRequest();
        }
    }
}
