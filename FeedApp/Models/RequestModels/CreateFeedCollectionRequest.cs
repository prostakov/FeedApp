using System.ComponentModel.DataAnnotations;

namespace FeedApp.Models.RequestModels
{
    public class CreateFeedCollectionRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
