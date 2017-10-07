using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Contracts
{
    public interface ITownRepository : IRepository<Town>
    {
        IQueryable<Town> GetTownsByCountryId(Guid countryId);
    }
}
