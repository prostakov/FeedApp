using System;
using System.ComponentModel.DataAnnotations;

namespace FeedApp.Models.RequestModels
{
    public class CreateFeedLabelRequest
    {
        [Required]
        [Display(Name = "FeedCollectionId")]
        public Guid FeedCollectionId { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Url")]
        public string Url { get; set; }
    }
}
