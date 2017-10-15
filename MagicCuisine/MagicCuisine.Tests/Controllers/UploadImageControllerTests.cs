using MagicCuisine.Controllers;
using MagicCuisine.Helpers.Contracts;
using MagicCuisine.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MagicCuisine.Tests.Controllers
{
    [TestFixture]
    public class UploadImageControllerTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullFileHelperIsPassedAsParameter()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UploadImageController(null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidValuesArePassedAsParameters()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();

            // Act & Assert
            Assert.DoesNotThrow(() => new UploadImageController(fileHelper.Object));
        }

        [Test]
        public void UploadActionShould_ReturnNotNullViewResult()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var origin = "origin";
            // Act
            ViewResult result = controller.Upload(origin) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UploadActionShould_CallViewWithoutName()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var origin = "origin";
            // Act
            ViewResult result = controller.Upload(origin) as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void UploadActionShould_CallViewWithoutModel()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var origin = "origin";
            // Act
            ViewResult result = controller.Upload(origin) as ViewResult;

            // Assert
            Assert.AreEqual(null, result.Model);
        }

        [Test]
        public void UploadActionShould_PassOriginParameterToViewBag()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var origin = "origin";
            // Act
            ViewResult result = controller.Upload(origin) as ViewResult;

            // Assert
            Assert.AreEqual(origin, result.ViewBag.origin);
        }

        public void _UploadActionShould_ReturnNotNullPartialViewResult()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            // Act
            PartialViewResult result = controller._Upload() as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void _UploadActionShould_CallPartialViewWithoutName()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            // Act
            PartialViewResult result = controller._Upload() as PartialViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void _UploadPostActionShould_CallFileHelperMethodUploadFile_WithFileList()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var files = new List<HttpPostedFileBase>();
            var origin = "origin";
            var model = new UploadViewModel(true, "location", null, origin);

            fileHelper.Setup(f => f.UploadFile(It.IsAny<IEnumerable<HttpPostedFileBase>>(), It.IsAny<string>())).Returns(model);
            // Act
            controller._Upload(files, origin);

            // Assert
            fileHelper.Verify(f => f.UploadFile(files, It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void _UploadPostActionShould_CallFileHelperMethodUploadFile_WithOrigin()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var files = new List<HttpPostedFileBase>();
            var origin = "origin";
            var model = new UploadViewModel(true, "location", null, origin);

            fileHelper.Setup(f => f.UploadFile(It.IsAny<IEnumerable<HttpPostedFileBase>>(), It.IsAny<string>())).Returns(model);

            // Act
            controller._Upload(files, origin);

            // Assert
            fileHelper.Verify(f => f.UploadFile(It.IsAny<IEnumerable<HttpPostedFileBase>>(), origin), Times.Once);
        }

        [Test]
        public void _UploadPostActionShould_ReturnJSON_UploadViewModelResult()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var files = new List<HttpPostedFileBase>();
            var origin = "origin";
            var model = new UploadViewModel(true, "location", null, origin);

            fileHelper.Setup(f => f.UploadFile(It.IsAny<IEnumerable<HttpPostedFileBase>>(), It.IsAny<string>())).Returns(model);

            // Act
            var result = controller._Upload(files, origin) as JsonResult;

            // Assert
            Assert.AreEqual(model.ToJson(), result.Data);
        }

        [Test]
        public void SaveActionShould_CallFileHelperMethodCropImage_WithPassedParameters()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var origin = "origin";
            var model = new UploadViewModel(true, "location", null, origin);

            fileHelper.Setup(f => f.CropImage(It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>())).Returns(model);

            string t = "t1";
            string l = "l1";
            string h = "h1";
            string w = "w1";
            string fileName = "filename";
            // Act
            var result = controller.Save(t, l, h, w, fileName, origin) as JsonResult;

            // Assert
            fileHelper.Verify(f => f.CropImage(t, l, h, w, fileName, origin), Times.Once);
        }

        [Test]
        public void SaveActionShould_ReturnJSON_UploadViewModelResult()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var origin = "origin";
            var model = new UploadViewModel(true, "location", null, origin);

            fileHelper.Setup(f => f.CropImage(It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>())).Returns(model);

            string t = "t1";
            string l = "l1";
            string h = "h1";
            string w = "w1";
            string fileName = "filename";
            // Act
            var result = controller.Save(t, l, h, w, fileName, origin) as JsonResult;

            // Assert
            Assert.AreEqual(model.ToJson(), result.Data);
        }

        [Test]
        public void SaveActionShould_ReturnAdd_AvatarLocationInTempData()
        {
            // Arrange
            var fileHelper = new Mock<IFileHelper>();
            var controller = new UploadImageController(fileHelper.Object);

            var origin = "origin";
            var model = new UploadViewModel(true, "location", null, origin);

            fileHelper.Setup(f => f.CropImage(It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>(),
                                               It.IsAny<string>())).Returns(model);

            string t = "t1";
            string l = "l1";
            string h = "h1";
            string w = "w1";
            string fileName = "filename";
            // Act
            var result = controller.Save(t, l, h, w, fileName, origin) as JsonResult;

            // Assert
            Assert.AreEqual(controller.TempData["avatar"], model.AvatarFileLocation);
        }
    }
}
