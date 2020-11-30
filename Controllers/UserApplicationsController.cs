using JobOffersMVC.Enums;
using JobOffersMVC.Filters;
using JobOffersMVC.Services.Helpers;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels;
using JobOffersMVC.ViewModels.JobOffers;
using JobOffersMVC.ViewModels.UserApplications;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class UserApplicationsController : Controller
    {
        private readonly IJobOffersService jobOffersService;
        private readonly IUsersService usersService;
        private readonly IUserApplicationsService userApplicationsService;
        private readonly IEmailService emailService;

        public UserApplicationsController(IJobOffersService jobOffersService, IUsersService usersService, IUserApplicationsService userApplicationsService, IEmailService emailService)
        {
            this.jobOffersService = jobOffersService;
            this.usersService = usersService;
            this.userApplicationsService = userApplicationsService;
            this.emailService = emailService;
        }

        public IActionResult Apply(int? jobOfferId)
        {
            if (!jobOfferId.HasValue)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserDetailsViewModel user = usersService.GetDetails(HttpContext.Session.GetInt32("loggedUserId").Value);

            JobOfferDetailsViewModel jobOffer = jobOffersService.GetDetails(jobOfferId.Value, user.Id);
            if (jobOffer == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            UserApplicationEditViewModel model = new UserApplicationEditViewModel
            {
                UserId = user.Id, // AuthenticationService.LoggedUser.Id,
                JobOfferId = jobOffer.Id, // jobOfferId.Value
                Status = ApplicationStatusEnum.Pending
            };

            userApplicationsService.Insert(model);

            // Send email
            emailService.SendAsync(new EmailViewModel
            {
                UserName = jobOffer.User.FullName,
                UserEmail = jobOffer.User.Email,
                Subject = "New Application",
                Body = $"{user.FullName} applied for job {jobOffer.Title}"
            });

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

            // User who created user application
            UserDetailsViewModel user = usersService.GetDetails(model.UserId);

            JobOfferEditViewModel jobOffer = jobOffersService.GetById(model.JobOfferId, user.Id);
            if (jobOffer == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            userApplicationsService.Update(model);

            emailService.SendAsync(new EmailViewModel
            {
                UserName = user.FullName,
                UserEmail = user.Email,
                Subject = "Application Accepted",
                Body = $"Your application for job {jobOffer.Title} has been accepted."
            });

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

            // User who created user application
            UserDetailsViewModel user = usersService.GetDetails(model.UserId);

            JobOfferEditViewModel jobOffer = jobOffersService.GetById(model.JobOfferId, user.Id);
            if (jobOffer == null)
            {
                return RedirectToAction("List", "UserJobOffers");
            }

            userApplicationsService.Update(model);

            emailService.SendAsync(new EmailViewModel
            {
                UserName = user.FullName,
                UserEmail = user.Email,
                Subject = "Application Rejected",
                Body = $"You application for job {jobOffer.Title} has been rejected."
            });

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
