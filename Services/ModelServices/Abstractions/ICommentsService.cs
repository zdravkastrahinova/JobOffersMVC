using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.Comments;

namespace JobOffersMVC.Services.ModelServices.Abstractions
{
    public interface ICommentsService : IBaseService<Comment, CommentDetailsViewModel, CommentEditViewModel>
    {
    }
}
