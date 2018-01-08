﻿using System;
using System.Threading.Tasks;
using FeedApp.Data;
using FeedApp.Models;
using FeedApp.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FeedApp.Controllers
{
    [Route("/api/accounts")]
    public class AccountsController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationTokenProvider _tokenProvider;

        public AccountsController(UserManager<ApplicationUser> userManager, AuthenticationTokenProvider tokenProvider)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }

        [HttpPost]
        [ValidateModel]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult(result.Errors);
            }
        }

        [HttpPost]
        [ValidateModel]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var authToken = _tokenProvider.GenerateToken(user.NormalizedUserName);

                return new OkObjectResult(new {Token = authToken});
            }

            return new BadRequestObjectResult("Combination of entered user/password does not exist!");
        }
    }

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}