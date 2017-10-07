using Data.Models;
using Services.Model;
using System;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IAddressService
    {
        ICollection<Country> GetAllCountries();

        ICollection<Town> GetTownsByCountryId(Guid countryId);

        Address CreateAddress(AddressServiceModel model);
    }
}
