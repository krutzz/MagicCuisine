using Data.Models;
using System;
using System.Linq;

namespace Data.Repository.Contracts
{
    public interface ITownRepository : IRepository<Town>
    {
        IQueryable<Town> GetTownsByCountryId(Guid countryId);
    }
}
