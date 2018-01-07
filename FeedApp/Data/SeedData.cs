using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedApp.Data;
using FeedApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace FeedApp.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var userName = "DefaultUser";

            var user = await userManager.FindByNameAsync(userName);
            
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, userName + "1234");

                if (!result.Succeeded) throw new Exception("Error initializing default users!");
            }
        }

        public static IWebHost Seed(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>())
                {
                    SeedAsync(userManager).GetAwaiter().GetResult();
                }
            }

            return webhost;
        }
    }
}
