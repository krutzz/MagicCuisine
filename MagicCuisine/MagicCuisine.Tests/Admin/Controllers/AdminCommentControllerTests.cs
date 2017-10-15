using Data.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MagicCuisine.Areas.Admin.Controllers;
using MagicCuisine.Areas.Admin.Models;
using MagicCuisine.Providers;
using Moq;
using NUnit.Framework;
using Services.Contracts;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MagicCuisine.Tests.Admin.Controllers
{
    [TestFixture]
    public class AdminCommentControllerTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullCommentServiceIsPassedAsParameter()
        {
            //Arrange
            var mapProvider = new Mock<IMapProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentController(null, mapProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullMapProviderIsPassedAsParameter()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentController(commentService.Object, null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidArePassedAsParameters()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            //Act & Assert
            Assert.DoesNotThrow(() => new CommentController(commentService.Object, mapProvider.Object));
        }

        [Test]
        public void GetCommentsShould_CallCommentServiceMethodGetAll()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();
            var sut = new CommentController(commentService.Object, mapProvider.Object);

            var request = new DataSourceRequest();
            var comments = new List<Comment>();
            commentService.Setup(c => c.GetAll()).Returns(comments);

            var commentViewList = new List<CommentViewModel>();
            mapProvider.Setup(m => m.GetMapCollection<CommentViewModel>(It.IsAny<Object>())).Returns(commentViewList);

            //Act 
            sut.GetComments(request);

            //Assert
            commentService.Verify(c => c.GetAll(), Times.Once);
        }

        [Test]
        public void GetCommentsShould_CallMapperProviderMethodGetMapCollection()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();
            var sut = new CommentController(commentService.Object, mapProvider.Object);

            var request = new DataSourceRequest();
            var comments = new List<Comment>();
            commentService.Setup(c => c.GetAll()).Returns(comments);

            var commentViewList = new List<CommentViewModel>();
            mapProvider.Setup(m => m.GetMapCollection<CommentViewModel>(It.IsAny<Object>())).Returns(commentViewList);

            //Act 
            sut.GetComments(request);

            //Assert
            mapProvider.Verify(m => m.GetMapCollection<CommentViewModel>(comments), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void UpdateCommentsShould_CallCommentServiceMethodEditComment(bool isDeleted)
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();
            var sut = new CommentController(commentService.Object, mapProvider.Object);

            var description = "Description";
            var isDeletedP = isDeleted;
            var model = new CommentViewModel()
            {
                Description = description,
                IsDeleted = isDeletedP
            };

            //Act 
            sut.UpdateComment(model);

            //Assert
            commentService.Verify(c => c.EditComment(model.ID, It.Is<CommentServiceModel>(s => s.Description == description && s.IsDeleted == isDeletedP)), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void UpdateCommentsShould_ReturnJSON_WithModelParameter(bool isDeleted)
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();
            var sut = new CommentController(commentService.Object, mapProvider.Object);

            var description = "Description";
            var isDeletedP = isDeleted;
            var model = new CommentViewModel()
            {
                Description = description,
                IsDeleted = isDeletedP
            };

            //Act 
            var result = sut.UpdateComment(model) as JsonResult;

            var data = result.Data as IList<CommentViewModel>;

            //Assert
            Assert.AreEqual(model, data[0]);
        }

        [Test]
        public void DeleteCommentsShould_CallCommentServiceMethodDeleteComment()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();
            var sut = new CommentController(commentService.Object, mapProvider.Object);

            var description = "Description";
            var model = new CommentViewModel()
            {
                Description = description
            };

            //Act 
            sut.DeleteComment(model);

            //Assert
            commentService.Verify(c => c.DeleteComment(model.ID), Times.Once);
        }

        [Test]
        public void DeleteCommentsShould_ReturnJSON_WithModelParameter()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();
            var sut = new CommentController(commentService.Object, mapProvider.Object);

            var description = "Description";
            var model = new CommentViewModel()
            {
                Description = description
            };

            //Act 
            var result = sut.DeleteComment(model) as JsonResult;

            var data = result.Data as IList<CommentViewModel>;

            //Assert
            Assert.AreEqual(model, data[0]);
        }
    }
}
