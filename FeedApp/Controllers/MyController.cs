using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Controllers
{
    public class MyController : Controller
    {
        protected readonly ApplicationDbContext _dbContext;

        public MyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected IActionResult SaveDbChanges()
        {
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
