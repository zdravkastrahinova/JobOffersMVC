using System.ComponentModel.DataAnnotations;

namespace JobOffersMVC.ViewModels.JobOffers
{
    public class JobOfferEditViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
