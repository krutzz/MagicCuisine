using Data.Models;
using Data.Repository.Contracts;

namespace Data.Repository
{
    public class AddessRepository : Repository<Address>, IAddessRepository
    {
        public AddessRepository(CuisineDbContext context)
            : base(context)
        {
        }
    }
}
