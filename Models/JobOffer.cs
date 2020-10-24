namespace JobOffersMVC.Models
{
    public class JobOffer : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
