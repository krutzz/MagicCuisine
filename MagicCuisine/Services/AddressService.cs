using Data.Models;
using Data.Repository.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AddressService : IAddressService
    {
        private readonly ICountryRepository countryRepository;
        private readonly ITownRepository townRepository;

        public AddressService(ICountryRepository countryRepository, ITownRepository townRepository)
        {
            if (countryRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.countryRepository = countryRepository;

            if (townRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.townRepository = townRepository;
        }

        public ICollection<Country> GetAllCountries()
        {
            return this.countryRepository.GetAll().ToList();
        }

        public ICollection<Town> GetTownsByCountryId(int countryId)
        {
            return this.townRepository.GetTownsByCountryId(countryId).ToList();
        }
    }
}
