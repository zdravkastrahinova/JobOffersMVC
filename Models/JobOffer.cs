using System.Collections.Generic;
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
        public virtual User User { get; set; }

        public virtual ICollection<UserApplication> UserApplications { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
