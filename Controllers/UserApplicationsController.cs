using JobOffersMVC.Enums;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using JobOffersMVC.ViewModels.UserApplications;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class UserApplicationsController : Controller
    {
        private readonly IJobOffersService jobOffersService;
        private readonly IUserApplicationsService userApplicationsService;

        public UserApplicationsController(IJobOffersService jobOffersService, IUserApplicationsService userApplicationsService)
        {
            this.jobOffersService = jobOffersService;
            this.userApplicationsService = userApplicationsService;
        }

        public IActionResult Apply(int? jobOfferId)
        {
            if (!jobOfferId.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            JobOfferEditViewModel jobOffer = jobOffersService.GetById(jobOfferId.Value);
            if (jobOffer == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplicationEditViewModel model = new UserApplicationEditViewModel
            {
                UserId = AuthenticationService.LoggedUser.Id,
                JobOfferId = jobOffer.Id, // jobOfferId.Value
                Status = ApplicationStatusEnum.Pending
            };

            userApplicationsService.Insert(model);

            return RedirectToAction("Details", "UserJobOffers", new { id = jobOfferId });
        }

        public IActionResult Accept(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplicationEditViewModel model = userApplicationsService.GetById(id.Value);
            if (model == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            model.Status = ApplicationStatusEnum.Accepted;

            userApplicationsService.Update(model);

            return RedirectToAction("Details", "UserJobOffers", new { id = model.JobOfferId });
        }

        public IActionResult Reject(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplicationEditViewModel model = userApplicationsService.GetById(id.Value);
            if (model == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            model.Status = ApplicationStatusEnum.Rejected;

            userApplicationsService.Update(model);

            return RedirectToAction("Details", "UserJobOffers", new { id = model.JobOfferId });
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplicationEditViewModel model = userApplicationsService.GetById(id.Value);
            if (model == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            userApplicationsService.Delete(model.Id);

            return RedirectToAction("Details", "UserJobOffers", new { id = model.JobOfferId });
        }
    }
}
