using Data.Models;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IAddressService
    {
        ICollection<Country> GetAllCountries();

        ICollection<Town> GetTownsByCountryId(int countryId);
    }
}
