using System.Linq;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class JobOffersController : Controller
    {
        private readonly IJobOffersRepository jobOffersRepository;

        public JobOffersController(IJobOffersRepository jobOffersRepository)
        {
            this.jobOffersRepository = jobOffersRepository;
        }

        public IActionResult List()
        {
            JobOfferListViewModel model = new JobOfferListViewModel();
            model.JobOffers = jobOffersRepository
                .GetAll()
                .Select(jobOffer => new JobOfferDetailsViewModel
                {
                    Id = jobOffer.Id,
                    Title = jobOffer.Title,
                    Description = jobOffer.Description,
                    UserId = jobOffer.UserId // TODO Add UserName to DetailsModel
                })
                .ToList();

            return View(model);
        }
    }
}