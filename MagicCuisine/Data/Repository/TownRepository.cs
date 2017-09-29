using Data.Models;
using Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TownRepository : Repository<Town>, ITownRepository
    {
        public TownRepository(CuisineContext context)
            : base(context)
        {
        }

        private CuisineContext CuisineContext
        {
            get { return this.Context as CuisineContext; }
        }

        public IList<Town> GetTownsByCountryId(int countryId)
        {
            var townsList =
                    from towns in this.CuisineContext.Towns
                    join country in this.CuisineContext.Countries
                    on towns.Country equals country
                    where country.ID == countryId
                    select towns;

            return townsList.ToList();
        }
    }
}
