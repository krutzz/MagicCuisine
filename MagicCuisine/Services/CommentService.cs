using Data.Models;
using Data.Repository.Contracts;
using Data.UnitOfWork;
using Services.Contracts;
using Services.Model;
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

        public CommentService(IUserRepository userRepository, IRecipeRepository recipeRepository, ICommentRepository commentRepository, IUnitOfWork unitOfWork)
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
        }

        public void CreateComment(string userId, Guid recipeId, string description)
        {
            var user = this.userRepository.Get(userId);
            var recipe = this.recipeRepository.Get(recipeId);

            var comment = new Comment(user, recipe, description);

            this.commentRepository.Add(comment);
            this.unitOfWork.Complete();
        }

        public void DeleteComment(Guid commentId)
        {
            var comment = this.commentRepository.Get(commentId);
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
            comment.Description = description;
            comment.Date = DateTime.Now;
            this.commentRepository.Update(comment);

            this.unitOfWork.Complete();
        }

        public void EditComment(Guid commentId, CommentServiceModel model)
        {
            var comment = this.commentRepository.Get(commentId);
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
