namespace JobOffersMVC.Models
{
    public class Comment : BaseModel
    {
        public string Text { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int JobOfferId { get; set; }
        public virtual JobOffer JobOffer { get; set; }
    }
}
