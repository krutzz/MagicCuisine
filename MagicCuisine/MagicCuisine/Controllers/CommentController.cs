using MagicCuisine.Models;
using MagicCuisine.Providers;
using Microsoft.AspNet.Identity;
using Services.Contracts;
using System;
using System.Web.Mvc;

namespace MagicCuisine.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IMapProvider mapProvider;

        public CommentController(ICommentService commentService, IMapProvider mapProvider)
        {
            if (commentService == null)
            {
                throw new ArgumentNullException();
            }

            this.commentService = commentService;

            if (mapProvider == null)
            {
                throw new ArgumentNullException();
            }

            this.mapProvider = mapProvider;
        }

        // GET: Comment
        [HttpGet]
        public ActionResult Index(Guid recipeId)
        {
            var model = new CommentIndexViewModel()
            {
                RecipeId = recipeId
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CommentIndexViewModel model)
        {
            var userID = User.Identity.GetUserId();
            var recipeId = model.RecipeId;
            var description = model.Description;

            this.commentService.CreateComment(userID, recipeId, description);

            return RedirectToAction("Index", "Recipe", new { id = model.RecipeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid commentId, Guid recipeId)
        {
            this.commentService.DeleteComment(commentId);
            return RedirectToAction("Index", "Recipe", new { id = recipeId });
        }

        [HttpGet]
        public ActionResult Edit(Guid commentId)
        {
            var comment = this.commentService.GetCommentById(commentId);
            var commentModel = this.mapProvider.GetMap<CommentViewModel>(comment);

            return PartialView("_EditComment", commentModel);
        }

        [HttpPost]
        public ActionResult Edit(CommentViewModel model)
        {
            this.commentService.EditComment(model.ID, model.Description);

            return RedirectToAction("Index", "Recipe", new { id = model.RecipeID });
        }
    }
}