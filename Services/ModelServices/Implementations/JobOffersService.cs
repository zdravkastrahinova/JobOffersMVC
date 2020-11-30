using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.JobOffers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersMVC.Services.ModelServices.Implementations
{
    public class JobOffersService : BaseService<JobOffer, JobOfferDetailsViewModel, JobOfferEditViewModel>, IJobOffersService
    {
        private IHttpContextAccessor contextAccessor;

        public JobOffersService(IJobOffersRepository jobOffersRepository, IMapper mapper, IHttpContextAccessor contextAccessor)
            : base(jobOffersRepository, mapper)
        {
            this.contextAccessor = contextAccessor;
        }

        public override void Insert(JobOfferEditViewModel viewModel)
        {
            viewModel.UserId = contextAccessor.HttpContext.Session.GetInt32("loggedUserId").Value;

            base.Insert(viewModel);
        }

        public override void Update(JobOfferEditViewModel viewModel)
        {
            viewModel.UserId = contextAccessor.HttpContext.Session.GetInt32("loggedUserId").Value;

            base.Update(viewModel);
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

        public JobOfferDetailsViewModel GetDetails(int id, int userId)
        {
            JobOffer item = ((IJobOffersRepository)repository).GetDetails(id, userId);

            return mapper.Map<JobOfferDetailsViewModel>(item);
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
