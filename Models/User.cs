using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobOffersMVC.Models
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(40)]
        public string Username { get; set; }

        [Required]
        [MaxLength(40)]
        public string Password { get; set; }

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(40)]
        public string Email { get; set; }

        public virtual ICollection<JobOffer> JobOffers { get; set; }

        public virtual ICollection<UserApplication> UserApplications { get; set; }
    }
}
