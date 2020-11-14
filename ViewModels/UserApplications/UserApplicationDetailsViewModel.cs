namespace JobOffersMVC.ViewModels.UserApplications
{
    public class UserApplicationDetailsViewModel : BaseViewModel
    {
        public int UserId { get; set; }

        public int JobOfferId { get; set; }

        public string Status { get; set; }

        public string JobOfferTitle { get; set; }

        public string UserName { get; set; }
    }
}
