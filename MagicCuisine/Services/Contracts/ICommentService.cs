﻿using Data.Models;
using System;

namespace Services.Contracts
{
    public interface ICommentService
    {
        void CreateComment(string userId, Guid recipeId, string description);

        void DeleteComment(Guid commentId);

        Comment GetCommentById(Guid commentId);

        void EditComment(Guid commentId, string description);
    }
}
