using JobOffersMVC.ViewModels.UserApplications;
using System.Collections.Generic;

namespace JobOffersMVC.ViewModels.JobOffers
{
    public class JobOfferDetailsViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public List<UserApplicationDetailsViewModel> UserApplications { get; set; }
    }
}
