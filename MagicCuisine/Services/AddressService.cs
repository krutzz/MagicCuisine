using Data.Models;
using Data.Repository.Contracts;
using Data.UnitOfWork;
using Services.Contracts;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AddressService : IAddressService
    {
        private readonly ICountryRepository countryRepository;
        private readonly ITownRepository townRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAddessRepository addessRepository;

        public AddressService(ICountryRepository countryRepository, ITownRepository townRepository, IAddessRepository addessRepository, IUnitOfWork unitOfWork)
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

            if (addessRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.addessRepository = addessRepository;

            if (unitOfWork == null)
            {
                throw new ArgumentNullException();
            }

            this.unitOfWork = unitOfWork;
        }

        public ICollection<Country> GetAllCountries()
        {
            return this.countryRepository.GetAll().ToList();
        }

        public ICollection<Town> GetTownsByCountryId(Guid countryId)
        {
            return this.townRepository.GetTownsByCountryId(countryId).ToList();
        }

        public Address CreateAddress(AddressServiceModel model)
        {
            var country = this.countryRepository.Get(model.Country);
            var town = this.townRepository.Get(model.Town);

            var address = new Address(model.Street, model.Building, model.Entrance, model.Floor, model.Flat, model.PostalCode, country, town);

            this.addessRepository.Add(address);
            this.unitOfWork.Complete();

            return address;
        }

        public Address GetAddress(Guid addressId)
        {
            return this.addessRepository.Get(addressId);
        }
    }
}
