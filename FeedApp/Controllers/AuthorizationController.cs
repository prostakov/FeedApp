using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedApp.Controllers
{
    [Route("api/authorization")]
    public class AuthorizationController : Controller
    {
        private readonly AuthenticationTokenProvider _tokenProvider;

        public AuthorizationController(AuthenticationTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }
        
        // GET api/values
        [HttpGet]
        [Route("authorization")]
        public string GetAuthorizationToken()
        {
            return _tokenProvider.GenerateToken("DefaultUser");
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        [Route("secret")]
        public string GetSecret()
        {
            return "Secret";
        }
    }
}
