using Data.Models;
using Data.Repository.Contracts;
using Data.UnitOfWork;
using Moq;
using NUnit.Framework;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MagicCuisine.Tests.Services
{
    [TestFixture]
    public class RecipeServiceTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullRecipeRepositoryIsPassedAsParameter()
        {
            //Arrange
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RecipeService(null, unitOfWork.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullUnitOfWorkIsPassedAsParameter()
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RecipeService(recipeRepository.Object, null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidParametersArePassed()
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.DoesNotThrow(() => new RecipeService(recipeRepository.Object, unitOfWork.Object));
        }

        [Test]
        public void CreateRecipeShould_ThrowArgumentNullException_WhenNullValueIsPassedAsParameter()
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RecipeService(recipeRepository.Object, unitOfWork.Object);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => sut.CreateRecipe(null));
        }

        [Test]
        public void CreateRecipeShould_CallRecipeRepositoryMethodAdd_WhenValidValueIsPassedAsParameter()
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RecipeService(recipeRepository.Object, unitOfWork.Object);
            var recipe = new Recipe();

            //Act 
            sut.CreateRecipe(recipe);

            //Assert
            recipeRepository.Verify(r => r.Add(recipe), Times.Once);
        }

        [Test]
        public void CreateRecipeShould_CallUnitOfWorkMethodComplete_WhenValidValueIsPassedAsParameter()
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RecipeService(recipeRepository.Object, unitOfWork.Object);
            var recipe = new Recipe();

            //Act 
            sut.CreateRecipe(recipe);

            //Assert
            unitOfWork.Verify(u => u.Complete(), Times.Once);
        }

        [Test]
        public void GetByIdShould_CallRecipeRepositoryMethodGet_WhenValidValueIsPassedAsParameter()
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RecipeService(recipeRepository.Object, unitOfWork.Object);
            var recipeId = Guid.NewGuid();

            //Act 
            sut.GetById(recipeId);

            //Assert
            recipeRepository.Verify(r => r.Get(recipeId), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetAllShould_CallRecipeRepositoryMethodFind_WhenValidValueIsPassedAsParameter(bool isDeleted)
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();

            var unitOfWork = new Mock<IUnitOfWork>();
            var sut = new RecipeService(recipeRepository.Object, unitOfWork.Object);

            //Act 
            sut.GetAll(isDeleted);

            //Assert
            recipeRepository.Verify(x => x.Find(It.IsAny<Expression<Func<Recipe, bool>>>()), Times.Once);
        }
    }
}
