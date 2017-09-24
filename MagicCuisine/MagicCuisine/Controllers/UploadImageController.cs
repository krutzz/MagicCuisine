using MagicCuisine.Helpers.Contracts;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MagicCuisine.Controllers
{
    public class UploadImageController : Controller
    {
        private readonly IFileHelper fileHelper;

        public UploadImageController(IFileHelper fileHelper)
        {
            this.fileHelper = fileHelper;
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult _Upload()
        {
            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult _Upload(IEnumerable<HttpPostedFileBase> files)
        {
            var result = this.fileHelper.UploadFile(files);
            return Json(result.ToJson());
        }

        [HttpPost]
        public ActionResult Save(string t, string l, string h, string w, string fileName)
        {
            var result = this.fileHelper.CropImage(t, l, h, w, fileName);
            if (result.success)
            {
                TempData["avatar"] = result.avatarFileLocation;
            }
            return Json(result.ToJson());
        }
    }
}