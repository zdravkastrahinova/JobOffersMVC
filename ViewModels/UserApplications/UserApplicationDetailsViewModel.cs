namespace JobOffersMVC.ViewModels.UserApplications
{
    public class UserApplicationDetailsViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int JobOfferId { get; set; }

        public string Status { get; set; }

        public string JobOfferTitle { get; set; }

        public string UserName { get; set; }
    }
}
