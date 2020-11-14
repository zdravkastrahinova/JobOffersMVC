using JobOffersMVC.Enums;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services;
using JobOffersMVC.Services.ModelServices.Abstractions;
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
        private readonly IJobOffersService jobOffersService;

        public UserJobOffersController(IJobOffersService jobOffersService)
        {
            this.jobOffersService = jobOffersService;
        }

        public IActionResult List()
        {
            JobOfferListViewModel model = new JobOfferListViewModel();

            model.JobOffers = jobOffersService.GetAllByUserId(AuthenticationService.LoggedUser.Id);

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            JobOfferDetailsViewModel model = jobOffersService.GetByIdWithUserApplications(id.Value, AuthenticationService.LoggedUser.Id);

            if (model == null)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View(new JobOfferEditViewModel());
            }

            JobOfferEditViewModel model = jobOffersService.GetById(id.Value, AuthenticationService.LoggedUser.Id);

            if (model == null)
            {
                return RedirectToAction("List");
            }

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

            if (model.Id == 0) // create
            {
                jobOffersService.Insert(model);

                return RedirectToAction("List");
            }

            // edit
            JobOfferEditViewModel jobOffer = jobOffersService.GetById(model.Id, AuthenticationService.LoggedUser.Id);
            if (jobOffer == null)
            {
                return RedirectToAction("List");
            }

            jobOffersService.Update(model);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            jobOffersService.Delete(id.Value, AuthenticationService.LoggedUser.Id);

            return RedirectToAction("List");
        }
    }
}
