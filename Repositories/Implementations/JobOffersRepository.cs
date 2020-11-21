using System.Collections.Generic;
using System.Linq;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobOffersMVC.Repositories.Implementations
{
    public class JobOffersRepository : BaseRepository<JobOffer>, IJobOffersRepository
    {
        public JobOffersRepository(JobOffersDbContext context)
            : base(context)
        {
            
        }

        public List<JobOffer> GetJobOffersWithUser()
        {
            return dbSet.Include(jo => jo.User).ToList();
        }

        public List<JobOffer> GetAllByUserId(int userId)
        {
            return GetAll().Where(o => o.UserId == userId).ToList();
        }

        public JobOffer GetById(int id, int userId)
        {
            return GetAll().FirstOrDefault(o => o.Id == id && o.UserId == userId);
        }

        public JobOffer GetByIdWithUserApplications(int id, int userId)
        {
            return dbSet.Include(jo => jo.UserApplications).Include(jo => jo.Comments).FirstOrDefault(jo => jo.Id == id && jo.UserId == userId);
        }

        public void Delete(int id, int userId)
        {
            JobOffer jobOffer = GetByIdWithUserApplications(id, userId);

            if (jobOffer == null)
            {
                return;
            }

            dbContext.Comments.RemoveRange(jobOffer.Comments);
            dbContext.UserApplications.RemoveRange(jobOffer.UserApplications);

            dbSet.Remove(jobOffer);

            dbContext.SaveChanges();
        }
    }
}