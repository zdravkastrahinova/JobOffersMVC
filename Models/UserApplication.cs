using JobOffersMVC.Enums;

namespace JobOffersMVC.Models
{
    public class UserApplication : BaseModel
    {
        public ApplicationStatusEnum Status { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int JobOfferId { get; set; }
        public virtual JobOffer JobOffer { get; set; }
    }
}
