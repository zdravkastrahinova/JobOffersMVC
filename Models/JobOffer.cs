using System.ComponentModel.DataAnnotations;

namespace JobOffersMVC.Models
{
    public class JobOffer : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
