using Data.Models;
using Data.Repository.Contracts;
using Data.UnitOfWork;
using Services.Contracts;
using Services.Model;
using Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentRepository commentRepository;
        private readonly IRecipeRepository recipeRepository;
        private readonly IUserRepository userRepository;
        private readonly IDateProvider dateProvider;

        public CommentService(IUserRepository userRepository,
                              IRecipeRepository recipeRepository,
                              ICommentRepository commentRepository,
                              IUnitOfWork unitOfWork,
                              IDateProvider dateProvider)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.userRepository = userRepository;

            if (recipeRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.recipeRepository = recipeRepository;

            if (commentRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.commentRepository = commentRepository;

            if (unitOfWork == null)
            {
                throw new ArgumentNullException();
            }
            this.unitOfWork = unitOfWork;

            if (dateProvider == null)
            {
                throw new ArgumentNullException();
            }

            this.dateProvider = dateProvider;
        }

        public void CreateComment(string userId, Guid recipeId, string description)
        {
            var user = this.userRepository.Get(userId);

            var recipe = this.recipeRepository.Get(recipeId);
            if (recipe == null)
            {
                throw new NullReferenceException("Recipe not found");
            }

            var comment = new Comment(user, recipe, description);

            this.commentRepository.Add(comment);
            this.unitOfWork.Complete();
        }

        public void DeleteComment(Guid commentId)
        {
            var comment = this.commentRepository.Get(commentId);
            if (comment == null)
            {
                throw new NullReferenceException("Comment not found");
            }

            comment.IsDeleted = true;
            this.commentRepository.Update(comment);

            this.unitOfWork.Complete();
        }

        public Comment GetCommentById(Guid commentId)
        {
            return this.commentRepository.Get(commentId);
        }

        public void EditComment(Guid commentId, string description)
        {
            var comment = this.commentRepository.Get(commentId);
            if (comment == null)
            {
                throw new NullReferenceException("Comment not found");
            }

            comment.Description = description;
            comment.Date = this.dateProvider.GetCurrentDate();
            this.commentRepository.Update(comment);

            this.unitOfWork.Complete();
        }

        public void EditComment(Guid commentId, CommentServiceModel model)
        {
            var comment = this.commentRepository.Get(commentId);
            if (comment == null)
            {
                throw new NullReferenceException("Comment not found");
            }

            comment.Description = model.Description;
            comment.IsDeleted = model.IsDeleted;
            this.commentRepository.Update(comment);

            this.unitOfWork.Complete();
        }

        public ICollection<Comment> GetAll()
        {
            return this.commentRepository.GetAll().ToList();
        }
    }
}
