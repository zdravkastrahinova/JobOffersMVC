using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobOffersMVC.ViewModels.Users
{
    public class UserDetailsViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{this.FirstName} {this.LastName}";

        public string ImagePath { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; }
    }
}
