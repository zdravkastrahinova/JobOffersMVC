using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;

namespace JobOffersMVC.Repositories.Implementations
{
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(JobOffersDbContext context)
            : base(context)
        {
        }
    }
}
