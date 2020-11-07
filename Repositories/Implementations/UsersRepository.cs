using System.Linq;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;

namespace JobOffersMVC.Repositories.Implementations
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(JobOffersDbContext context)
            : base (context)
        {
            
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().FirstOrDefault(user => user.Username == username && user.Password == password);
        }
    }
}