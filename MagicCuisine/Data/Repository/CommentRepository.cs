using Data.Models;
using Data.Repository.Contracts;

namespace Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(CuisineDbContext context)
            : base(context)
        {
        }

    }
}
