using FeedApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeedApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FeedCollection> FeedCollections { get; set; }

        public DbSet<FeedLabel> FeedLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new FeedCollectionConfiguration(builder.Entity<FeedCollection>());

            new FeedLabelConfiguration(builder.Entity<FeedLabel>());
        }
    }
}
