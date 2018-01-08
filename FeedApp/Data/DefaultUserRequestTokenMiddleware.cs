using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FeedApp.Data
{
    public class DefaultUserRequestTokenMiddleware
    {
        // The middleware delegate to call after this one finishes processing
        private readonly RequestDelegate _next;

        private readonly AuthenticationTokenProvider _tokenProvider;

        public DefaultUserRequestTokenMiddleware(RequestDelegate next, AuthenticationTokenProvider tokenProvider)
        {
            _next = next;
            _tokenProvider = tokenProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine($"Request for {httpContext.Request.Path} received ({httpContext.Request.ContentLength ?? 0} bytes)");

            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                httpContext.Request.Headers.Add("Authorization", "Bearer " + _tokenProvider.GenerateToken("DefaultUser"));
            }

            // Call the next middleware delegate in the pipeline 
            await _next.Invoke(httpContext);
        }
    }
}
