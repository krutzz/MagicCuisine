using MagicCuisine.Controllers;
using MagicCuisine.Models;
using MagicCuisine.Providers;
using Moq;
using NUnit.Framework;
using Services.Contracts;
using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Data.Models;
using System.Security.Principal;
using System.Web;
using System.Security.Claims;
using System.Web.Routing;

namespace MagicCuisine.Tests.Controllers
{
    [TestFixture]
    public class CommentControllerTests
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
        public void IndexActionShould_CallView_WithCorrectCommentIndexViewModel()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();

            // Act
            PartialViewResult result = controller.Index(recipeId) as PartialViewResult;
            var model = result.Model as CommentIndexViewModel;

            // Assert
            Assert.AreEqual(recipeId, model.RecipeId);
        }

        [Test]
        public void IndexActionShould_CallView_WithoutViewName()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();

            // Act
            PartialViewResult result = controller.Index(recipeId) as PartialViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void IndexPostShould_CallCommentServiceMethodCreateComment_WithModelParameters()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            var description = "some description";

            var viewModel = new CommentIndexViewModel()
            {
                Description = description,
                RecipeId = recipeId
            };

            var context = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("test@gmail.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            controller.Index(viewModel);

            //Assert
            commentService.Verify(c => c.CreateComment(It.IsAny<string>(), recipeId, description), Times.Once);
        }

        [Test]
        public void IndexPostShould_RedirectTo_RecipeControler_IndexAction()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            var description = "some description";

            var viewModel = new CommentIndexViewModel()
            {
                Description = description,
                RecipeId = recipeId
            };

            var context = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("test@gmail.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            var result = controller.Index(viewModel) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Recipe", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void IndexPostShould_RedirectTo_RecipeControler_IndexAction_WithIdParameter()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            var description = "some description";

            var viewModel = new CommentIndexViewModel()
            {
                Description = description,
                RecipeId = recipeId
            };

            var context = new Mock<HttpContextBase>();
            var identity = new GenericIdentity("test@gmail.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            context.Setup(s => s.User).Returns(principal);

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            // Act
            var result = controller.Index(viewModel) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(recipeId, result.RouteValues["id"]);
        }

        [Test]
        public void DeleteShould_CallCommentServiceMethodDeleteCommet_WithCommentId()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            var commentId = Guid.NewGuid();

            // Act
            controller.Delete(commentId, recipeId);

            //Assert
            commentService.Verify(c => c.DeleteComment(commentId), Times.Once);
        }

        [Test]
        public void DeleteShould_RedirectTo_RecipeControler_IndexAction()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            var commentId = Guid.NewGuid();

            // Act
            var result = controller.Delete(commentId, recipeId) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Recipe", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void DeleteShould_RedirectTo_RecipeControler_IndexAction_WithIdParameter()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var recipeId = Guid.NewGuid();
            var commentId = Guid.NewGuid();

            // Act
            var result = controller.Delete(commentId, recipeId) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(recipeId, result.RouteValues["id"]);
        }

        [Test]
        public void EditShould_CallCommentServiceMethodGetCommentById_WithCommentId()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var commentId = Guid.NewGuid();

            var comment = new Comment();
            commentService.Setup(c => c.GetCommentById(It.IsAny<Guid>())).Returns(comment);
            var commentViewModel = new CommentViewModel();
            mapProvider.Setup(m => m.GetMap<CommentViewModel>(It.IsAny<Object>())).Returns(commentViewModel);

            // Act
            controller.Edit(commentId);

            //Assert
            commentService.Verify(c => c.GetCommentById(commentId), Times.Once);
        }

        [Test]
        public void EditShould_ReturnPartialView__EditComment()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var commentId = Guid.NewGuid();

            var comment = new Comment();
            commentService.Setup(c => c.GetCommentById(It.IsAny<Guid>())).Returns(comment);
            var commentViewModel = new CommentViewModel();
            mapProvider.Setup(m => m.GetMap<CommentViewModel>(It.IsAny<Object>())).Returns(commentViewModel);

            // Act
            var result = controller.Edit(commentId) as PartialViewResult;

            //Assert
            Assert.AreEqual("_EditComment", result.ViewName);
        }

        [Test]
        public void EditShould_CallPartialView_WithCommentViewModel()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var commentId = Guid.NewGuid();

            var comment = new Comment();
            commentService.Setup(c => c.GetCommentById(It.IsAny<Guid>())).Returns(comment);
            var commentViewModel = new CommentViewModel();
            mapProvider.Setup(m => m.GetMap<CommentViewModel>(It.IsAny<Object>())).Returns(commentViewModel);

            // Act
            var result = controller.Edit(commentId) as PartialViewResult;

            //Assert
            Assert.AreEqual(commentViewModel, result.Model);
        }

        [Test]
        public void EditPostShould_CallCommentServiceMethodEditCommet_WithCommentId()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "description";
            var model = new CommentViewModel()
            {
                ID = commentId,
                Description = description
            };

            // Act
            controller.Edit(model);

            //Assert
            commentService.Verify(c => c.EditComment(commentId, It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void EditPostShould_CallCommentServiceMethodEditCommet_WithDescription()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "description";
            var model = new CommentViewModel()
            {
                ID = commentId,
                Description = description
            };

            // Act
            controller.Edit(model);

            //Assert
            commentService.Verify(c => c.EditComment(It.IsAny<Guid>(), description), Times.Once);
        }

        [Test]
        public void EditPostShould_RedirectTo_RecipeControler_IndexAction()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "description";
            var model = new CommentViewModel()
            {
                ID = commentId,
                Description = description
            };

            // Act
            var result = controller.Edit(model) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Recipe", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void EditPostShould_RedirectTo_RecipeControler_IndexAction_WithIdParameter()
        {
            //Arrange
            var commentService = new Mock<ICommentService>();
            var mapProvider = new Mock<IMapProvider>();

            var controller = new CommentController(commentService.Object, mapProvider.Object);

            var commentId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();
            var description = "description";
            var model = new CommentViewModel()
            {
                ID = commentId,
                Description = description,
                RecipeID = recipeId
            };

            // Act
            var result = controller.Edit(model) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(recipeId, result.RouteValues["id"]);
        }
    }
}
