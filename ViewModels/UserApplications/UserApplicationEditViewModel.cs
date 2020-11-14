using JobOffersMVC.Enums;

namespace JobOffersMVC.ViewModels.UserApplications
{
    public class UserApplicationEditViewModel : BaseViewModel
    {
        public ApplicationStatusEnum Status { get; set; }

        public int UserId { get; set; }

        public int JobOfferId { get; set; }
    }
}
