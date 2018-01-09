using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FeedApp.Data;
using FeedApp.Models;
using FeedApp.Models.RequestModels;
using FeedApp.Models.ResourceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedApp.Controllers
{
    [Authorize]
    [Route("/api/collections")]
    public class CollectionsController : MyController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        public CollectionsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
            IMapper mapper) : base(dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IEnumerable<FeedCollectionResource>> Get()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollections = _dbContext.FeedCollections
                .Include(f => f.Feeds)
                .Where(f => f.UserId == user.Id)
                .OrderBy(f => f.DateAdded);

            return _mapper.Map<IEnumerable<FeedCollection>, IEnumerable<FeedCollectionResource>>(feedCollections);
        }
        
        [HttpPost]
        [ValidateRequest]
        public async Task<IActionResult> Create(CreateFeedCollectionRequest request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollection = new FeedCollection
            {
                Name = request.Name,
                UserId = user.Id,
                DateAdded = DateTime.Now
            };

            _dbContext.FeedCollections.Add(feedCollection);

            return SaveDbChanges();
        }

        [HttpPut]
        [ValidateRequest]
        public async Task<IActionResult> Update(UpdateFeedCollectionRequest request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollection = await _dbContext.FeedCollections
                .Include(f => f.User).FirstOrDefaultAsync(f => f.Id == request.Id);

            if (feedCollection.User.Id == user.Id)
            {
                feedCollection.Name = request.Name;

                return SaveDbChanges();
            }

            return Forbid();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid collectionId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollection = await _dbContext.FeedCollections
                .Include(f => f.User).FirstOrDefaultAsync(f => f.Id == collectionId);

            if (feedCollection.User.Id == user.Id)
            {
                _dbContext.FeedCollections.Remove(feedCollection);

                return SaveDbChanges();
            }

            return Forbid();
        }
    }
}
