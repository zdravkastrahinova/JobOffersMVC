using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.JobOffers;
using System.Collections.Generic;

namespace JobOffersMVC.Services.ModelServices.Abstractions
{
    public interface IJobOffersService : IBaseService<JobOffer, JobOfferDetailsViewModel, JobOfferEditViewModel>
    {
        List<JobOfferDetailsViewModel> GetJobOffersWithUser();

        List<JobOfferDetailsViewModel> GetAllByUserId(int userId);

        JobOfferDetailsViewModel GetByIdWithUserApplications(int id, int userId);

        JobOfferEditViewModel GetById(int id, int userId);

        void Delete(int id, int userId);
    }
}
