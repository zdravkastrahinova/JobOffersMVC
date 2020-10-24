using JobOffersMVC.Repositories;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using JobOffersMVC.Filters;
using JobOffersMVC.Models;
using JobOffersMVC.Services;

namespace JobOffersMVC.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class JobOffersController : Controller
    {
        public IActionResult List()
        {
            JobOffersRepository jobOffersRepository = new JobOffersRepository();
            
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

            JobOffersRepository jobOffersRepository = new JobOffersRepository();
            JobOffer jobOffer = jobOffersRepository.GetById(id.Value, AuthenticationService.LoggedUser.Id);

            if (jobOffer == null)
            {
                return RedirectToAction("List");
            }

            JobOfferDetailsViewModel model = new JobOfferDetailsViewModel
            {
                Id = jobOffer.Id,
                Title = jobOffer.Title,
                Description = jobOffer.Description,
                UserId = jobOffer.UserId
            };

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View(new JobOfferEditViewModel());
            }

            JobOffersRepository jobOffersRepository = new JobOffersRepository();
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

            JobOffersRepository jobOffersRepository = new JobOffersRepository();

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

            JobOffersRepository jobOffersRepository = new JobOffersRepository();
            jobOffersRepository.Delete(id.Value, AuthenticationService.LoggedUser.Id);

            return RedirectToAction("List");
        }
    }
}
