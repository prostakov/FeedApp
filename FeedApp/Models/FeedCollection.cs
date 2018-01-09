using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeedApp.Models
{
    public class FeedCollection
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime DateAdded { get; set; }

        public ICollection<FeedLabel> Feeds { get; set; }
    }

    public class FeedCollectionConfiguration
    {
        public FeedCollectionConfiguration(EntityTypeBuilder<FeedCollection> builder)
        {
            builder.ToTable("FeedCollections");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.DateAdded)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.FeedCollections)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            builder.HasMany(p => p.Feeds)
                .WithOne(f => f.FeedCollection)
                .HasForeignKey(f => f.FeedCollectionId)
                .IsRequired();
        }
    }
}
