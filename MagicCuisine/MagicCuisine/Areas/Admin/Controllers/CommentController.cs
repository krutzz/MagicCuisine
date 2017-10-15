using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MagicCuisine.Areas.Admin.Models;
using Services.Contracts;
using System;
using System.Web.Mvc;
using Services.Model;
using MagicCuisine.Providers;

namespace MagicCuisine.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public ActionResult GetComments([DataSourceRequest] DataSourceRequest request)
        {
            var comments = this.commentService.GetAll();

            var commentViewList = this.mapProvider.GetMapCollection<CommentViewModel>(comments);

            var result = commentViewList.ToDataSourceResult(request);

            return Json(result);
        }

        public ActionResult UpdateComment(CommentViewModel model)
        {
            var serviceModel = new CommentServiceModel(model.Description, model.IsDeleted);
            this.commentService.EditComment(model.ID, serviceModel);

            return Json(new[] { model });
        }

        public ActionResult DeleteComment(CommentViewModel model)
        {
            this.commentService.DeleteComment(model.ID);

            return Json(new[] { model });
        }
    }
}