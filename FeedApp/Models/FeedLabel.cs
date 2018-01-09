using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeedApp.Models
{
    public class FeedLabel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime DateAdded { get; set; }

        public Guid FeedCollectionId { get; set; }

        public FeedCollection FeedCollection { get; set; }
    }

    public class FeedLabelConfiguration
    {
        public FeedLabelConfiguration(EntityTypeBuilder<FeedLabel> builder)
        {
            builder.ToTable("FeedLabels");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Url)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.DateAdded)
                .IsRequired();
        }
    }
}
