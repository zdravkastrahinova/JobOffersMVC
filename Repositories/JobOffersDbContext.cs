using JobOffersMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace JobOffersMVC.Repositories
{
    public class JobOffersDbContext : DbContext
    {
        public JobOffersDbContext(DbContextOptions<JobOffersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
    }
}
