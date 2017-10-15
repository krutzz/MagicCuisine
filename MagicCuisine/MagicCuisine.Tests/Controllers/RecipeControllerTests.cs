using Data.Models;
using MagicCuisine.Controllers;
using MagicCuisine.Models;
using MagicCuisine.Providers;
using Moq;
using NUnit.Framework;
using Services.Contracts;
using System;
using System.Web.Mvc;

namespace MagicCuisine.Tests.Controllers
{
    [TestFixture]
    public class RecipeControllerTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullRecipeServiceIsPassedAsParameter()
        {
            // Arrange
            var mapProvider = new Mock<IMapProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RecipeController(null, mapProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullMapProviderIsPassedAsParameter()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RecipeController(recipeService.Object, null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidValuesArePassedAsParameters()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new RecipeController(recipeService.Object, mapProvider.Object));
        }

        [Test]
        public void IndexActionShould_CallRecipeServiceMethodGetById_WithRecipeIdParameter()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            // Act
            controller.Index(recipeId);

            // Assert
            recipeService.Verify(r => r.GetById(recipeId), Times.Once);
        }

        [Test]
        public void IndexActionShould_CallMapProviderMethodGetMap_WithRecipeParameter()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();

            var recipe = new Recipe();
            recipeService.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(recipe);

            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            // Act
            controller.Index(recipeId);

            // Assert
            mapProvider.Verify(m => m.GetMap<RecipeViewModel>(recipe), Times.Once);
        }

        [Test]
        public void IndexActionShould_ReturnNotNullViewResult()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            // Act
            ViewResult result = controller.Index(recipeId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexActionShould_CallView_WithCorrectRecipeIndexViewModel()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();

            var recipeViewModel = new RecipeViewModel();
            mapProvider.Setup(m => m.GetMap<RecipeViewModel>(It.IsAny<Object>())).Returns(recipeViewModel);

            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();

            // Act
            ViewResult result = controller.Index(recipeId) as ViewResult;
            var model = result.Model as RecipeIndexViewModel;

            // Assert
            Assert.AreEqual(recipeViewModel, model.Recipe);
        }

        [Test]
        public void IndexActionShould_CallView_WithoutViewName()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();

            var recipeViewModel = new RecipeViewModel();
            mapProvider.Setup(m => m.GetMap<RecipeViewModel>(It.IsAny<Object>())).Returns(recipeViewModel);

            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();

            // Act
            ViewResult result = controller.Index(recipeId) as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
