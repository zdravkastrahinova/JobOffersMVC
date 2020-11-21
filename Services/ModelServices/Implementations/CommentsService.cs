using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.Comments;
using Microsoft.AspNetCore.Http;

namespace JobOffersMVC.Services.ModelServices.Implementations
{
    public class CommentsService : BaseService<Comment, CommentDetailsViewModel, CommentEditViewModel>, ICommentsService
    {
        private IHttpContextAccessor contextAccessor;

        public CommentsService(ICommentsRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor)
            : base(repository, mapper)
        {
            this.contextAccessor = contextAccessor;
        }

        public override void Insert(CommentEditViewModel viewModel)
        {
            viewModel.UserId = contextAccessor.HttpContext.Session.GetInt32("loggedUserId").Value;

            base.Insert(viewModel);
        }

        public override void Update(CommentEditViewModel viewModel)
        {
            viewModel.UserId = contextAccessor.HttpContext.Session.GetInt32("loggedUserId").Value;

            base.Update(viewModel);
        }
    }
}
