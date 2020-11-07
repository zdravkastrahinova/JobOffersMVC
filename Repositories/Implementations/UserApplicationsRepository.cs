using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;

namespace JobOffersMVC.Repositories.Implementations
{
    public class UserApplicationsRepository : BaseRepository<UserApplication>, IUserApplicationsRepository
    {
        public UserApplicationsRepository(JobOffersDbContext context)
            : base(context) { }
    }
}
