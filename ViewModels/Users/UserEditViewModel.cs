using System.ComponentModel.DataAnnotations;

namespace JobOffersMVC.ViewModels.Users
{
    public class UserEditViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Cannot enter username greater than 30 characters")]
        public string Username { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Password must contain at least 5 characters")]
        [MaxLength(30, ErrorMessage = "Password cannot be more that 30 characters")]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [MaxLength(30, ErrorMessage = "First name cannot be more that 30 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(30, ErrorMessage = "Last name cannot be more that 30 characters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(30, ErrorMessage = "Email cannot be more than 30 characters")]
        public string Email { get; set; }
    }
}
