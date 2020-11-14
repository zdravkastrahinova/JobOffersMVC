using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.ModelServices.Implementations
{
    public class JobOffersService : BaseService<JobOffer, JobOfferDetailsViewModel, JobOfferEditViewModel>, IJobOffersService
    {
        public JobOffersService(IJobOffersRepository jobOffersRepository, IMapper mapper)
            : base(jobOffersRepository, mapper)
        {

        }

        public void Delete(int id, int userId)
        {
            ((IJobOffersRepository)repository).Delete(id, userId);
        }

        public List<JobOfferDetailsViewModel> GetAllByUserId(int userId)
        {
            return ((IJobOffersRepository)repository)
                .GetAllByUserId(userId)
                .Select(model => mapper.Map<JobOffer, JobOfferDetailsViewModel>(model))
                .ToList();
        }

        public JobOfferEditViewModel GetById(int id, int userId)
        {
            JobOffer jobOffer = ((IJobOffersRepository)repository).GetById(id, userId);

            return mapper.Map<JobOfferEditViewModel>(jobOffer);
        }

        public JobOfferDetailsViewModel GetByIdWithUserApplications(int id, int userId)
        {
            JobOffer jobOffer = ((IJobOffersRepository)repository).GetByIdWithUserApplications(id, userId);

            return mapper.Map<JobOfferDetailsViewModel>(jobOffer);
        }

        public List<JobOfferDetailsViewModel> GetJobOffersWithUser()
        {
            return ((IJobOffersRepository)repository)
                .GetJobOffersWithUser()
                .Select(model => mapper.Map<JobOffer, JobOfferDetailsViewModel>(model))
                .ToList();
        }
    }
}
