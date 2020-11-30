using JobOffersMVC.ViewModels.Comments;
using JobOffersMVC.ViewModels.UserApplications;
using JobOffersMVC.ViewModels.Users;
using System.Collections.Generic;

namespace JobOffersMVC.ViewModels.JobOffers
{
    public class JobOfferDetailsViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public UserDetailsViewModel User { get; set; }

        public List<UserApplicationDetailsViewModel> UserApplications { get; set; }

        public List<CommentDetailsViewModel> Comments { get; set; }
    }
}
