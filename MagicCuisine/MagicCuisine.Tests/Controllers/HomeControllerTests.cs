using Data.Models;
using MagicCuisine.Controllers;
using MagicCuisine.Models;
using MagicCuisine.Providers;
using Moq;
using NUnit.Framework;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MagicCuisine.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullRecipeServiceIsPassedAsParameter()
        {
            //Arrange
            var mapProvider = new Mock<IMapProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(null, mapProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullMapProviderIsPassedAsParameter()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(recipeService.Object, null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidArePassedAsParameters()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();

            //Act & Assert
            Assert.DoesNotThrow(() => new HomeController(recipeService.Object, mapProvider.Object));
        }

        [Test]
        public void IndexShould_CallRecipeServiceMethodGetALL_WithFalseAsParameter()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new HomeController(recipeService.Object, mapProvider.Object);

            var recipeList = new List<Recipe>();
            recipeService.Setup(r => r.GetAll(It.IsAny<bool>())).Returns(recipeList);

            //Act
            controller.Index();

            //Assert
            recipeService.Verify(r => r.GetAll(false), Times.Once);
        }

        [Test]
        public void IndexShould_CallMapProviderMethodGetMapOnce_WithRecipeAsParameter()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new HomeController(recipeService.Object, mapProvider.Object);

            var recipe = new Recipe();
            var recipeList = new List<Recipe>()
            {
                recipe
            };
            recipeService.Setup(r => r.GetAll(It.IsAny<bool>())).Returns(recipeList);

            var recipeViewModel = new RecipeViewModel();
            mapProvider.Setup(m => m.GetMap<RecipeViewModel>(It.IsAny<Object>())).Returns(recipeViewModel);
            //Act
            controller.Index();

            //Assert
            mapProvider.Verify(r => r.GetMap<RecipeViewModel>(recipe), Times.Once);
        }

        [Test]
        public void IndexShould_ReturnView_WithoutViewName()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new HomeController(recipeService.Object, mapProvider.Object);

            var recipe = new Recipe();
            var recipeList = new List<Recipe>()
            {
                recipe
            };
            recipeService.Setup(r => r.GetAll(It.IsAny<bool>())).Returns(recipeList);

            var recipeViewModel = new RecipeViewModel();
            mapProvider.Setup(m => m.GetMap<RecipeViewModel>(It.IsAny<Object>())).Returns(recipeViewModel);
            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestCase("Content/Images/general/logo.png")]
        public void IndexShould_ReturnView_WithDefaultLogo(string expectedLogo)
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new HomeController(recipeService.Object, mapProvider.Object);

            var recipe = new Recipe();
            var recipeList = new List<Recipe>()
            {
                recipe
            };
            recipeService.Setup(r => r.GetAll(It.IsAny<bool>())).Returns(recipeList);

            var recipeViewModel = new RecipeViewModel();
            mapProvider.Setup(m => m.GetMap<RecipeViewModel>(It.IsAny<Object>())).Returns(recipeViewModel);
            //Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as HomeIndexViewModel;

            //Assert
            Assert.AreEqual(expectedLogo, model.LogoImg);
        }

        [Test]
        public void IndexShould_ReturnView_WithRecipeList()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new HomeController(recipeService.Object, mapProvider.Object);

            var recipe = new Recipe();
            var recipeList = new List<Recipe>()
            {
                recipe
            };
            recipeService.Setup(r => r.GetAll(It.IsAny<bool>())).Returns(recipeList);

            var recipeViewModel = new RecipeViewModel();
            mapProvider.Setup(m => m.GetMap<RecipeViewModel>(It.IsAny<Object>())).Returns(recipeViewModel);
            //Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as HomeIndexViewModel;
            var expecteList = new List<RecipeViewModel>()
            {
                recipeViewModel
            };

            //Assert
            CollectionAssert.AreEqual(expecteList, model.Recipes);
        }
    }
}
