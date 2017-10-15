using Data.Models;
using Data.Repository.Contracts;
using Moq;
using NUnit.Framework;
using Services;
using System;

namespace MagicCuisine.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullUserRepositoryIsPassedAsParameter()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidUserRepositoryIsPassedAsParameter()
        {
            //Arrange
            var userRepository = new Mock<IUserRepository>();

            //Act & Assert
            Assert.DoesNotThrow(() => new UserService(userRepository.Object));
        }

        [Test]
        public void GetUserShould_ThrowArgumentNullException_WhenNullUserIdIsPassedAsParameter()
        {
            //Arrange
            var userRepository = new Mock<IUserRepository>();
            var sut = new UserService(userRepository.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => sut.GetUser(null));
        }

        [Test]
        public void GetUserShould_CallUserRepositoryMethodGet_WhenValidUserIdIsPassedAsParameter()
        {
            //Arrange
            var userRepository = new Mock<IUserRepository>();
            var sut = new UserService(userRepository.Object);

            // Act 
            var userId = "validUser";
            sut.GetUser(userId);

            //Assert
            userRepository.Verify(u => u.Get(userId), Times.Once);
        }

        [Test]
        public void GetUserShould_ReturnUser_WhenValidUserIdIsPassedAsParameter()
        {
            //Arrange
            var userRepository = new Mock<IUserRepository>();
            var user = new User()
            {
                Email = "test@gmail.com"
            };
            userRepository.Setup(u => u.Get(It.IsAny<string>())).Returns(user);

            var sut = new UserService(userRepository.Object);

            // Act 
            var userId = "validUser";
            var result = sut.GetUser(userId);

            //Assert
            Assert.AreEqual(user, result);
        }
    }
}
