using JobOffersMVC.Models;

namespace JobOffersMVC.Repositories.Abstractions
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);
    }
}
