namespace JobOffersMVC.ViewModels.Comments
{
    public class CommentDetailsViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public int JobOfferId { get; set; }
        public string Text { get; set; }
        public string JobOferTitle { get; set; }
        public string UserName { get; set; }
    }
}
