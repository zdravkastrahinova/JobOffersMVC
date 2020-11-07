using JobOffersMVC.Enums;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services;
using JobOffersMVC.ViewModels.JobOffers;
using JobOffersMVC.ViewModels.UserApplications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class UserJobOffersController : Controller
    {
        private readonly IJobOffersRepository jobOffersRepository;

        public UserJobOffersController(IJobOffersRepository jobOffersRepository)
        {
            this.jobOffersRepository = jobOffersRepository;
        }

        public IActionResult List()
        {
            JobOfferListViewModel model = new JobOfferListViewModel();
            model.JobOffers = jobOffersRepository
                .GetAllByUserId(AuthenticationService.LoggedUser.Id)
                .Select(o => new JobOfferDetailsViewModel
                {
                    Id = o.Id,
                    Title = o.Title,
                    Description = o.Description,
                    UserId = o.UserId
                })
                .ToList();

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            JobOffer jobOffer = jobOffersRepository.GetByIdWithUserApplications(id.Value, AuthenticationService.LoggedUser.Id);

            if (jobOffer == null)
            {
                return RedirectToAction("List");
            }

            JobOfferDetailsViewModel model = new JobOfferDetailsViewModel
            {
                Id = jobOffer.Id,
                Title = jobOffer.Title,
                Description = jobOffer.Description,
                UserId = jobOffer.UserId,
                UserApplications = new UserApplicationListViewModel
                {
                    UserApplications = jobOffer.UserApplications.Select(application => new UserApplicationDetailsViewModel
                    {
                        Id = application.Id,
                        UserId = application.UserId,
                        JobOfferId = application.JobOfferId,
                        UserName = application.User.FirstName + " " + application.User.LastName,
                        JobOfferTitle = application.JobOffer.Title,
                        Status = Enum.GetName(typeof (ApplicationStatusEnum), application.Status)
                    }).ToList()
                }
            };

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View(new JobOfferEditViewModel());
            }

            JobOffer jobOffer = jobOffersRepository.GetById(id.Value, AuthenticationService.LoggedUser.Id);

            if (jobOffer == null)
            {
                return RedirectToAction("List");
            }

            JobOfferEditViewModel model = new JobOfferEditViewModel
            {
                Id = jobOffer.Id,
                Title = jobOffer.Title,
                Description = jobOffer.Description,
                UserId = jobOffer.UserId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JobOfferEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            JobOffer jobOffer;
            if (model.Id == 0) // create
            {
                jobOffer = new JobOffer
                {
                    Title = model.Title,
                    Description = model.Description,
                    UserId = AuthenticationService.LoggedUser.Id
                };

                jobOffersRepository.Insert(jobOffer);

                return RedirectToAction("List");
            }

            // edit
            jobOffer = jobOffersRepository.GetById(model.Id, AuthenticationService.LoggedUser.Id);
            if (jobOffer == null)
            {
                return RedirectToAction("List");
            }

            jobOffer.Title = model.Title;
            jobOffer.Description = model.Description;

            jobOffersRepository.Update(jobOffer);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            jobOffersRepository.Delete(id.Value, AuthenticationService.LoggedUser.Id);

            return RedirectToAction("List");
        }
    }
}
