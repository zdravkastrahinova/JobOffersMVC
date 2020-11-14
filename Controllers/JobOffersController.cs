using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Mvc;

namespace JobOffersMVC.Controllers
{
    public class JobOffersController : Controller
    {
        private readonly IJobOffersService jobOffersService;

        public JobOffersController(IJobOffersService jobOffersService)
        {
            this.jobOffersService = jobOffersService;
        }

        public IActionResult List()
        {
            JobOfferListViewModel model = new JobOfferListViewModel();

            model.JobOffers = jobOffersService.GetJobOffersWithUser();

            return View(model);
        }
    }
}