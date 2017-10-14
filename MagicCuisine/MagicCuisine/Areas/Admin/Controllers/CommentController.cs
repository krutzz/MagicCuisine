using AutoMapper;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MagicCuisine.Areas.Admin.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Services.Model;

namespace MagicCuisine.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            if (commentService == null)
            {
                throw new ArgumentNullException();
            }

            this.commentService = commentService;
        }

        public ActionResult GetComments([DataSourceRequest] DataSourceRequest request)
        {
            var comments = this.commentService.GetAll();

            var commentViewList = Mapper.Map<ICollection<CommentViewModel>>(comments);

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