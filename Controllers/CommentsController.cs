using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.Comments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobOffersMVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        public IActionResult Edit(string value)
        {
            CommentEditViewModel model = JsonConvert.DeserializeObject<CommentEditViewModel>(value);

            if (model.Id == 0)
            {
                commentsService.Insert(model);
            }
            else
            {
                commentsService.Update(model);
            }

            return RedirectToAction("Details", "UserJobOffers", new { id = model.JobOfferId });
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            CommentEditViewModel model = commentsService.GetById(id.Value);
            if (model == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            commentsService.Delete(model.Id);

            return RedirectToAction("Details", "UserJobOffers", new { id = model.JobOfferId });
        }
    }
}
