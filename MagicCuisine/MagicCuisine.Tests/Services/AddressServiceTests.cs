using Data.Models;
using Data.Repository.Contracts;
using Data.UnitOfWork;
using Moq;
using NUnit.Framework;
using Services;
using Services.Model;
using System;
using System.Linq;

namespace MagicCuisine.Tests.Services
{
    [TestFixture]
    public class AddressServiceTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullCountryRepositoryIsPassedAsParameter()
        {
            //Arrange
            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AddressService(null,
                                                                        townRepository.Object,
                                                                        addessRepository.Object,
                                                                        unitOfWork.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullTownRepositoryIsPassedAsParameter()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AddressService(countryRepository.Object,
                                                                        null,
                                                                        addessRepository.Object,
                                                                        unitOfWork.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullAddressRepositoryIsPassedAsParameter()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AddressService(countryRepository.Object,
                                                                        townRepository.Object,
                                                                        null,
                                                                        unitOfWork.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullUnitOfWorkIsPassedAsParameter()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AddressService(countryRepository.Object,
                                                                        townRepository.Object,
                                                                        addessRepository.Object,
                                                                        null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidParametersArePassed()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.DoesNotThrow(() => new AddressService(countryRepository.Object,
                                                                        townRepository.Object,
                                                                        addessRepository.Object,
                                                                        unitOfWork.Object));
        }

        [Test]
        public void GetAllCountriesShould_CallCountryRepositoryMothodGetAll()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);

            //Act 
            sut.GetAllCountries();

            //Assert
            countryRepository.Verify(c => c.GetAll(), Times.Once);
        }

        [Test]
        public void GetAllCountriesShould_ReturnCollectionWithoutFormating()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();

            var countries = new Country[] {
                                      new Country()
                                      {
                                          Name = "Bulgaria"
                                      }
                                        };
            var queryableCountries = countries.AsQueryable();

            countryRepository.Setup(c => c.GetAll()).Returns(queryableCountries);


            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);

            //Act 
            var result = sut.GetAllCountries();

            //Assert
            CollectionAssert.AreEqual(countries, result);
        }

        [Test]
        public void GetTownsByCountryIdShould_CallTownRepositoryMothodGetTownsByCountryIdWithPassedParameter()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var countryId = Guid.NewGuid();

            //Act 
            sut.GetTownsByCountryId(countryId);

            //Assert
            townRepository.Verify(t => t.GetTownsByCountryId(countryId), Times.Once);
        }

        [Test]
        public void GetTownsByCountryIdShould_ReturnCollectionWithoutFormating()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();

            var towns = new Town[] {
                                      new Town()
                                      {
                                          Name = "Sofia",
                                          Country = new Country(){
                                                                  Name = "Bulgaria"
                                                                  }
                                      }
                                   };

            var queryableTowns = towns.AsQueryable();

            townRepository.Setup(t => t.GetTownsByCountryId(It.IsAny<Guid>())).Returns(queryableTowns);

            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var countryId = Guid.NewGuid();

            //Act 
            var result = sut.GetTownsByCountryId(countryId);

            //Assert
            CollectionAssert.AreEqual(towns, result);
        }

        [Test]
        public void GetAddressShould_CallAddressRepositoryMothodGetWithPassedParameter()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var addressId = Guid.NewGuid();

            //Act 
            sut.GetAddress(addressId);

            //Assert
            addessRepository.Verify(a => a.Get(addressId), Times.Once);
        }

        [Test]
        public void CreateAddressShould_ThrowArgumentNullException_WhenNullValueIsPassed()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => sut.CreateAddress(null));
        }

        [Test]
        public void CreateAddressShould_ThrowNullReferenceException_WhenInvalidCountryIsPassed()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            countryRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns((Country)null);

            var townRepository = new Mock<ITownRepository>();
            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var model = new AddressServiceModel();

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => sut.CreateAddress(model));
        }

        [Test]
        public void CreateAddressShould_ThrowNullReferenceException_WhenInvalidTownIsPassed()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var country = new Country()
            {
                Name = "Bulgaria"
            };

            countryRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(country);

            var townRepository = new Mock<ITownRepository>();
            townRepository.Setup(t => t.Get(It.IsAny<Guid>())).Returns((Town)null);

            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var model = new AddressServiceModel();

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => sut.CreateAddress(model));
        }

        [Test]
        public void CreateAddressShould_CallAddressRepositoryMethodAdd_WhenValidParametersArePassed()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var country = new Country()
            {
                Name = "Bulgaria"
            };

            countryRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(country);

            var townRepository = new Mock<ITownRepository>();
            var town = new Town()
            {
                Name = "Sofia",
                Country = country
            };

            townRepository.Setup(t => t.Get(It.IsAny<Guid>())).Returns(town);

            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var model = new AddressServiceModel()
            {
                Street = "1",
                Building = "2",
                Entrance = "A",
                Floor = "9",
                Flat = "62",
                PostalCode = "1715"
            };

            //Act 
            var result = sut.CreateAddress(model);

            //Assert
            addessRepository.Verify(a => a.Add(It.IsAny<Address>()), Times.Once);
        }

        [Test]
        public void CreateAddressShould_ReturnCorrectAddress_WhenValidParametersArePassed()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var country = new Country()
            {
                Name = "Bulgaria"
            };

            countryRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(country);

            var townRepository = new Mock<ITownRepository>();
            var town = new Town()
            {
                Name = "Sofia",
                Country = country
            };

            townRepository.Setup(t => t.Get(It.IsAny<Guid>())).Returns(town);

            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var model = new AddressServiceModel()
            {
                Street = "1",
                Building = "2",
                Entrance = "A",
                Floor = "9",
                Flat = "62",
                PostalCode = "1715"
            };

            var addressExpected = new Address(model.Street, model.Building, model.Entrance, model.Floor, model.Flat, model.PostalCode, country, town);

            //Act 
            var result = sut.CreateAddress(model);

            //Assert
            Assert.AreEqual(addressExpected.Street, result.Street);
            Assert.AreEqual(addressExpected.Building, result.Building);
            Assert.AreEqual(addressExpected.Entrance, result.Entrance);
            Assert.AreEqual(addressExpected.Floor, result.Floor);
            Assert.AreEqual(addressExpected.Flat, result.Flat);
            Assert.AreEqual(addressExpected.PostalCode, result.PostalCode);
            Assert.AreEqual(addressExpected.Country, result.Country);
            Assert.AreEqual(addressExpected.Town, result.Town);
        }

        [Test]
        public void CreateAddressShould_CallUnitOfWorkMethodComplete_WhenValidParametersArePassed()
        {
            //Arrange
            var countryRepository = new Mock<ICountryRepository>();
            var country = new Country()
            {
                Name = "Bulgaria"
            };

            countryRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(country);

            var townRepository = new Mock<ITownRepository>();
            var town = new Town()
            {
                Name = "Sofia",
                Country = country
            };

            townRepository.Setup(t => t.Get(It.IsAny<Guid>())).Returns(town);

            var addessRepository = new Mock<IAddessRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new AddressService(countryRepository.Object, townRepository.Object, addessRepository.Object, unitOfWork.Object);
            var model = new AddressServiceModel()
            {
                Street = "1",
                Building = "2",
                Entrance = "A",
                Floor = "9",
                Flat = "62",
                PostalCode = "1715"
            };

            //Act 
            var result = sut.CreateAddress(model);

            //Assert
            unitOfWork.Verify(u => u.Complete(), Times.Once);
        }
    }
}
