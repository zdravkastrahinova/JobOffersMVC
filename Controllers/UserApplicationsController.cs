using JobOffersMVC.Enums;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class UserApplicationsController : Controller
    {
        private readonly IJobOffersRepository jobOffersRepository;
        private readonly IUserApplicationsRepository userApplicationsRepository;

        public UserApplicationsController(IJobOffersRepository jobOffersRepository, IUserApplicationsRepository userApplicationsRepository)
        {
            this.jobOffersRepository = jobOffersRepository;
            this.userApplicationsRepository = userApplicationsRepository;
        }

        public IActionResult Apply(int? jobOfferId)
        {
            if (!jobOfferId.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            JobOffer jobOffer = jobOffersRepository.GetById(jobOfferId.Value);
            if (jobOffer == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            userApplicationsRepository.Insert(new UserApplication
            {
                UserId = AuthenticationService.LoggedUser.Id,
                JobOfferId = jobOffer.Id, // jobOfferId.Value
                Status = ApplicationStatusEnum.Pending
            });

            return RedirectToAction("Details", "UserJobOffers", new { id = jobOfferId });
        }

        public IActionResult Accept(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplication application = userApplicationsRepository.GetById(id.Value);
            if (application == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            application.Status = ApplicationStatusEnum.Accepted;

            userApplicationsRepository.Update(application);

            return RedirectToAction("Details", "UserJobOffers", new { id = application.JobOfferId });
        }

        public IActionResult Reject(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplication application = userApplicationsRepository.GetById(id.Value);
            if (application == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            application.Status = ApplicationStatusEnum.Rejected;

            userApplicationsRepository.Update(application);

            return RedirectToAction("Details", "UserJobOffers", new { id = application.JobOfferId });
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplication application = userApplicationsRepository.GetById(id.Value);
            if (application == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            userApplicationsRepository.Delete(application.Id);

            return RedirectToAction("Details", "UserJobOffers", new { id = application.JobOfferId });
        }
    }
}
