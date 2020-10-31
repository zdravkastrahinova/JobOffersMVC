using System.Collections.Generic;
using System.Linq;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;

namespace JobOffersMVC.Repositories
{
    public class JobOffersRepository : BaseRepository<JobOffer>, IJobOffersRepository
    {
        public JobOffersRepository(JobOffersDbContext context)
            : base(context)
        {
            
        }

        public List<JobOffer> GetAllByUserId(int userId)
        {
            return GetAll().Where(o => o.UserId == userId).ToList();
        }

        public JobOffer GetById(int id, int userId)
        {
            return GetAll().FirstOrDefault(o => o.Id == id && o.UserId == userId);
        }

        public void Delete(int id, int userId)
        {
            JobOffer jobOffer = GetById(id, userId);

            if (jobOffer == null)
            {
                return;
            }

            base.Delete(jobOffer.Id);
        }
    }
}