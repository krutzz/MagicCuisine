﻿using MagicCuisine.Helpers.Contracts;
using System;
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
            if (fileHelper == null)
            {
                throw new ArgumentNullException();
            }

            this.fileHelper = fileHelper;
        }

        public ActionResult Upload(string origin)
        {
            ViewBag.origin = origin;
            return View();
        }

        public ActionResult _Upload()
        {
            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult _Upload(IEnumerable<HttpPostedFileBase> files, string origin)
        {
            var result = this.fileHelper.UploadFile(files, origin);
            return Json(result.ToJson());
        }

        [HttpPost]
        public ActionResult Save(string t, string l, string h, string w, string fileName, string origin)
        {
            var result = this.fileHelper.CropImage(t, l, h, w, fileName, origin);
            if (result.Success)
            {
                TempData["avatar"] = result.AvatarFileLocation;
            }
            return Json(result.ToJson());
        }
    }
}