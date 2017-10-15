using Data.Models;
using MagicCuisine.Areas.Admin.Controllers;
using MagicCuisine.Areas.Admin.Models;
using MagicCuisine.Providers;
using Moq;
using NUnit.Framework;
using Services.Contracts;
using System;
using System.Web.Mvc;

namespace MagicCuisine.Tests.Admin.Controllers
{
    [TestFixture]
    public class AdminRecipeControllerTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullRecipeServiceIsPassedAsParameter()
        {
            //Arrange
            var mapProvider = new Mock<IMapProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RecipeController(null, mapProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullMapProviderIsPassedAsParameter()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RecipeController(recipeService.Object, null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidArePassedAsParameters()
        {
            //Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();

            //Act & Assert
            Assert.DoesNotThrow(() => new RecipeController(recipeService.Object, mapProvider.Object));
        }

        [Test]
        public void IndexActionShould_ReturnNotNullViewResult()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexActionShould_CallViewWithoutName()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestCase(null, "/Avatars/img-default.png")]
        [TestCase("location", "location")]
        public void IndexActionShould_CallViewWithCorrectModel(string avatar, string expectedAvatar)
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);
            controller.TempData["avatar"] = avatar;

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var model = result.Model as IndexViewModel;

            // Assert
            Assert.AreEqual(expectedAvatar, model.Avatar);
        }

        [Test]
        public void IndexPostActionShould_CallMapProviderMethodGetMap_WithModelParameter()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var model = new IndexViewModel();
            // Act
            ViewResult result = controller.Index(model) as ViewResult;

            // Assert
            mapProvider.Verify(m => m.GetMap<Recipe>(model), Times.Once);
        }

        [Test]
        public void IndexPostActionShould_CallRecipeServiceMethodCreateRecipe_WithTransformedRecipeModelParameter()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var model = new IndexViewModel();
            var recipe = new Recipe();
            mapProvider.Setup(m => m.GetMap<Recipe>(It.IsAny<Object>())).Returns(recipe);

            // Act
            ViewResult result = controller.Index(model) as ViewResult;

            // Assert
            recipeService.Verify(r => r.CreateRecipe(recipe), Times.Once);
        }

        [Test]
        public void IndexPostActionShould_RedirectToAction_HomeController_IndexAction()
        {
            // Arrange
            var recipeService = new Mock<IRecipeService>();
            var mapProvider = new Mock<IMapProvider>();
            var controller = new RecipeController(recipeService.Object, mapProvider.Object);

            var model = new IndexViewModel();
            var recipe = new Recipe();
            mapProvider.Setup(m => m.GetMap<Recipe>(It.IsAny<Object>())).Returns(recipe);

            // Act
            RedirectToRouteResult result = controller.Index(model) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }
    }
}
