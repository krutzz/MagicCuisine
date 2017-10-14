using Data.Models;
using Services.Model;
using System;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface ICommentService
    {
        void CreateComment(string userId, Guid recipeId, string description);

        void DeleteComment(Guid commentId);

        Comment GetCommentById(Guid commentId);

        void EditComment(Guid commentId, string description);

        void EditComment(Guid commentId, CommentServiceModel model);

        ICollection<Comment> GetAll();
    }
}
