using Data.Contracts;
using Data.Repository;
using Data.Repository.Contracts;

namespace Data
{
    public class DataBase : IDataBase
    {
        private readonly CuisineDbContext context;

        public DataBase(CuisineDbContext context)
        {
            this.context = context;

            this.Countries = new CountryRepository(context);
            this.Towns = new TownRepository(context);
            this.Addesses = new AddessRepository(context);
        }

        public ICountryRepository Countries { get; private set; }

        public ITownRepository Towns { get; private set; }

        public IAddessRepository Addesses { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}
