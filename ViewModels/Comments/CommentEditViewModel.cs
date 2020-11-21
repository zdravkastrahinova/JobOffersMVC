namespace JobOffersMVC.ViewModels.Comments
{
    public class CommentEditViewModel : BaseViewModel
    {
        public string Text { get; set; }

        public int JobOfferId { get; set; }

        public int? UserId { get; set; }
    }
}
