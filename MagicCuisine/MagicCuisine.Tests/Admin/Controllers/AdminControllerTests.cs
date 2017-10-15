using MagicCuisine.Areas.Admin.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace MagicCuisine.Tests.Admin.Controllers
{
    [TestFixture]
    public class AdminControllerTests
    {
        [Test]
        public void IndexActionShould_ReturnNotNullViewResult()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexActionShould_CallViewWithoutName()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void IndexActionShould_CallViewWithoutModel()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model);
        }
    }
}
