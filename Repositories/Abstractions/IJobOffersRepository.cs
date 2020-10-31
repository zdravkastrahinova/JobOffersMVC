using System.Collections.Generic;
using JobOffersMVC.Models;

namespace JobOffersMVC.Repositories.Abstractions
{
    public interface IJobOffersRepository : IBaseRepository<JobOffer>
    {
        List<JobOffer> GetAllByUserId(int userId);
        JobOffer GetById(int id, int userId);
        void Delete(int id, int userId);
    }
}
