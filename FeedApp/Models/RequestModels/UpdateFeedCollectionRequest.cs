using System;
using System.ComponentModel.DataAnnotations;

namespace FeedApp.Models.RequestModels
{
    public class UpdateFeedCollectionRequest
    {
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
