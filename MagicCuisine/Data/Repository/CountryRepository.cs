using Data.Models;
using Data.Repository.Contracts;

namespace Data.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(CuisineDbContext context)
            : base(context)
        {
        }
    }
}
