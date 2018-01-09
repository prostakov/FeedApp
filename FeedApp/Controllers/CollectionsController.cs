using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FeedApp.Data;
using FeedApp.Models;
using FeedApp.Models.ResourceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedApp.Controllers
{
    [Authorize]
    [Route("/api/collections")]
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        public CollectionsController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IEnumerable<FeedCollectionResource>> Get()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollections = _dbContext.FeedCollections.Where(f => f.UserId == user.Id).OrderBy(f => f.DateAdded);

            return _mapper.Map<IEnumerable<FeedCollection>, IEnumerable<FeedCollectionResource>>(feedCollections);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(string feedCollectionName)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var feedCollection = new FeedCollection
            {
                Name = feedCollectionName,
                UserId = user.Id,
                DateAdded = DateTime.Now
            };

            _dbContext.FeedCollections.Add(feedCollection);

            try
            {
                _dbContext.SaveChanges();

                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            
        }
    }
}
