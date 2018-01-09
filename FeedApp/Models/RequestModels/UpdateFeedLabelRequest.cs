using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FeedApp.Models.RequestModels
{
    public class UpdateFeedLabelRequest
    {
        [Required]
        [Display(Name = "FeedLabelId")]
        public Guid FeedLabelId { get; set; }
        
        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Url")]
        public string Url { get; set; }
    }
}
