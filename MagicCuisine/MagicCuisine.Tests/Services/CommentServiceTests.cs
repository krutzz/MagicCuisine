using Data.Models;
using Data.Repository.Contracts;
using Data.UnitOfWork;
using Moq;
using NUnit.Framework;
using Services;
using Services.Model;
using Services.Providers;
using System;

namespace MagicCuisine.Tests.Services
{
    [TestFixture]
    public class CommentServiceTests
    {
        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullUserRepositoryIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(null,
                                                              recipeRepository.Object,
                                                              commentRepository.Object,
                                                              unitOfWork.Object,
                                                              dateProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullRecipeRepositoryIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(userRepository.Object,
                              null,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullCommentRepositoryIsPassedAsParameter()
        {
            //Arrange
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              null,
                              unitOfWork.Object,
                              dateProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullUnitOfWorkIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              null,
                              dateProvider.Object));
        }

        [Test]
        public void ConstructorShould_ThrowArgumentNullException_WhenNullDateProviderIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidVluesArePassedAsParameters()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            //Act & Assert
            Assert.DoesNotThrow(() => new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object));
        }

        [Test]
        public void CreateCommentShould_ThrowArgumentNullException_WhenNullUserIdValueIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var recipeId = Guid.NewGuid();
            var description = "some valid description";

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => sut.CreateComment(null, recipeId, description));
        }

        [Test]
        public void CreateCommentShould_CallUserRepositoryGet_WhenValidUserIdValueIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Recipe());

            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var userId = "validUserId";
            var recipeId = Guid.NewGuid();
            var description = "some valid description";

            //Act
            sut.CreateComment(userId, recipeId, description);

            //Assert
            userRepository.Verify(u => u.Get(userId), Times.Once);
        }

        [Test]
        public void CreateCommentShould_ThrowNullReferenceException_WhenInvalidRecipeIdValueIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns((Recipe)null);

            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var userId = "validUserId";
            var recipeId = Guid.NewGuid();
            var description = "some valid description";

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => sut.CreateComment(userId, recipeId, description));
        }

        [Test]
        public void CreateCommentShould_CallRecipeRepositoryGet_WhenValidRecipeIdValueIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Recipe());

            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var userId = "validUserId";
            var recipeId = Guid.NewGuid();
            var description = "some valid description";

            //Act
            sut.CreateComment(userId, recipeId, description);

            //Assert
            recipeRepository.Verify(r => r.Get(recipeId), Times.Once);
        }

        [Test]
        public void CreateCommentShould_CallCommentRepositoryAdd_WhenValidValuesArePassedAsParameters()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Recipe());

            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var userId = "validUserId";
            var recipeId = Guid.NewGuid();
            var description = "some valid description";

            //Act
            sut.CreateComment(userId, recipeId, description);

            //Assert
            commentRepository.Verify(c => c.Add(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void CreateCommentShould_UnitOfWorkComplete_WhenValidValuesArePassedAsParameters()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Recipe());

            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var userId = "validUserId";
            var recipeId = Guid.NewGuid();
            var description = "some valid description";

            //Act
            sut.CreateComment(userId, recipeId, description);

            //Assert
            unitOfWork.Verify(u => u.Complete(), Times.Once);
        }

        [Test]
        public void DeleteCommentShould_CallCommentRepositoryGet_WhenValidCommentIdIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();

            //Act
            sut.DeleteComment(commentId);

            //Assert
            commentRepository.Verify(c => c.Get(commentId), Times.Once);
        }

        [Test]
        public void DeleteCommentShould_ThrowNullReferenceException_WhenInvalidCommentIdIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns((Comment)null);
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => sut.DeleteComment(commentId));
        }

        [Test]
        public void DeleteCommentShould_CallCommentRepositoryUpdate_WithDeletedCommentObjectAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();

            //Act
            sut.DeleteComment(commentId);

            //Assert
            commentRepository.Verify(c => c.Update(It.Is<Comment>(x => x.IsDeleted == true)), Times.Once);
        }

        [Test]
        public void DeleteCommentShould_UnitOfWorkComplete_WhenValidValuesArePassedAsParameters()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();

            //Act
            sut.DeleteComment(commentId);

            //Assert
            unitOfWork.Verify(u => u.Complete(), Times.Once);
        }

        [Test]
        public void GetCommentByIdShould_CallCommentRepositoryGet_WithCommentIdAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();

            //Act
            sut.GetCommentById(commentId);

            //Assert
            commentRepository.Verify(c => c.Get(commentId), Times.Once);
        }

        [Test]
        public void GetAllShould_CallCommentRepositoryGetAll()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            //Act
            sut.GetAll();

            //Assert
            commentRepository.Verify(c => c.GetAll(), Times.Once);
        }

        [Test]
        public void EditCommentShould_CallCommentRepositoryGet_WhenValidCommentIdIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";

            //Act
            sut.EditComment(commentId, description);

            //Assert
            commentRepository.Verify(c => c.Get(commentId), Times.Once);
        }

        [Test]
        public void EditCommentShould_ThrowNullReferenceException_WhenInvalidCommentIdIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns((Comment)null);
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => sut.EditComment(commentId, description));
        }

        [Test]
        public void EditCommentShould_CallCommentRepositoryUpdate_WhenValidCommentIdIsPassedAsParameter_WithDescription()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();

            var date = new DateTime(1900, 1, 1);
            dateProvider.Setup(d => d.GetCurrentDate()).Returns(date);
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";

            //Act
            sut.EditComment(commentId, description);

            //Assert
            commentRepository.Verify(c => c.Update(It.Is<Comment>(x => x.Description == description)), Times.Once);
        }

        [Test]
        public void EditCommentShould_CallCommentRepositoryUpdate_WhenValidCommentIdIsPassedAsParameter_WithCurrentDate()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();

            var date = new DateTime(1900, 1, 1);
            dateProvider.Setup(d => d.GetCurrentDate()).Returns(date);
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";

            //Act
            sut.EditComment(commentId, description);

            //Assert
            commentRepository.Verify(c => c.Update(It.Is<Comment>(x => x.Date == date)), Times.Once);
        }

        [Test]
        public void EditCommentShould_CallUnitOfWorkComplete_WhenValidValuesArePassedAsParameters()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";

            //Act
            sut.EditComment(commentId, description);

            //Assert
            unitOfWork.Verify(u => u.Complete(), Times.Once);
        }

        [Test]
        public void EditCommentWithCommentServiceModelShould_CallCommentRepositoryGet_WhenValidCommentIdIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";

            var model = new CommentServiceModel(description, true);

            //Act
            sut.EditComment(commentId, model);

            //Assert
            commentRepository.Verify(c => c.Get(commentId), Times.Once);
        }

        [Test]
        public void EditCommentWithCommentServiceModelShould_ThrowNullReferenceException_WhenInvalidCommentIdIsPassedAsParameter()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns((Comment)null);
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";
            var model = new CommentServiceModel(description, true);

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => sut.EditComment(commentId, model));
        }

        [Test]
        public void EditCommentWithCommentServiceModelShould_CallCommentRepositoryUpdate_WhenValidCommentIdIsPassedAsParameter_WithDescription()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();

            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";
            var model = new CommentServiceModel(description, true);

            //Act
            sut.EditComment(commentId, model);

            //Assert
            commentRepository.Verify(c => c.Update(It.Is<Comment>(x => x.Description == description)), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void EditCommentWithCommentServiceModelShould_CallCommentRepositoryUpdate_WhenValidCommentIdIsPassedAsParameter_WithIsDeleted(bool isDeleted)
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();

            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";
            var model = new CommentServiceModel(description, isDeleted);

            //Act
            sut.EditComment(commentId, model);

            //Assert
            commentRepository.Verify(c => c.Update(It.Is<Comment>(x => x.IsDeleted == isDeleted)), Times.Once);
        }

        [Test]
        public void EditCommentWithCommentServiceModelShould_CallUnitOfWorkComplete_WhenValidValuesArePassedAsParameters()
        {
            //Arrange
            var commentRepository = new Mock<ICommentRepository>();

            commentRepository.Setup(c => c.Get(It.IsAny<Guid>())).Returns(new Comment());
            var recipeRepository = new Mock<IRecipeRepository>();
            var userRepository = new Mock<IUserRepository>();
            var dateProvider = new Mock<IDateProvider>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var sut = new CommentService(userRepository.Object,
                              recipeRepository.Object,
                              commentRepository.Object,
                              unitOfWork.Object,
                              dateProvider.Object);

            var commentId = Guid.NewGuid();
            var description = "some description";
            var model = new CommentServiceModel(description, true);

            //Act
            sut.EditComment(commentId, model);

            //Assert
            unitOfWork.Verify(u => u.Complete(), Times.Once);
        }
    }
}
