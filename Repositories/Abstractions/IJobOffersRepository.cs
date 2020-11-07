﻿using System.Collections.Generic;
using JobOffersMVC.Models;

namespace JobOffersMVC.Repositories.Abstractions
{
    public interface IJobOffersRepository : IBaseRepository<JobOffer>
    {
        List<JobOffer> GetJobOffersWithUser();
        List<JobOffer> GetAllByUserId(int userId);
        JobOffer GetById(int id, int userId);

        JobOffer GetByIdWithUserApplications(int id, int userId);
        void Delete(int id, int userId);
    }
}
