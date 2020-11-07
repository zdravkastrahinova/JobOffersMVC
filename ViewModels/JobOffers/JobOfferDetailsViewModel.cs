using JobOffersMVC.ViewModels.UserApplications;

namespace JobOffersMVC.ViewModels.JobOffers
{
    public class JobOfferDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public UserApplicationListViewModel UserApplications { get; set; }
    }
}
