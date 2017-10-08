using MagicCuisine.Helpers.Contracts;
using MagicCuisine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;

namespace MagicCuisine.Helpers
{

    public class FileHelper : IFileHelper
    {
        private const int AvatarStoredWidth = 200;  // Change the size of the stored avatar image
        private const int AvatarStoredHeight = 200; // Change the size of the stored avatar image
        private const int AvatarScreenWidth = 300;  // Change the value of the width of the image on the screen

        private const string TempFolder = "/Temp";
        private const string MapTempFolder = "~" + TempFolder;
        private const string AvatarPath = "/Avatars";

        private static readonly string[] _imageFileExtensions = { ".jpg", ".png", ".gif", ".jpeg" };

        public UploadViewModel UploadFile(IEnumerable<HttpPostedFileBase> files, string origin)
        {
            if (files == null || !files.Any())
            {
                return new UploadViewModel(false, null, "No file uploaded.", origin);
            }

            var file = files.FirstOrDefault();  // get ONE only
            if (file == null || !IsImage(file))
            {
                return new UploadViewModel(false, null, "File is of wrong format.", origin);
            }

            if (file.ContentLength <= 0)
            {
                return new UploadViewModel(false, null, "File cannot be zero length.", origin);
            }
            var webPath = GetTempSavedFilePath(file);

            return new UploadViewModel(true, webPath.Replace("\\", "/"), null, origin); // success
        }

        public UploadViewModel CropImage(string t, string l, string h, string w, string fileName, string origin)
        {
            try
            {
                // Calculate dimensions
                var top = Convert.ToInt32(t.Replace("-", "").Replace("px", ""));
                var left = Convert.ToInt32(l.Replace("-", "").Replace("px", ""));
                var height = Convert.ToInt32(h.Replace("-", "").Replace("px", ""));
                var width = Convert.ToInt32(w.Replace("-", "").Replace("px", ""));

                // Get file from temporary folder
                var fn = Path.Combine(HostingEnvironment.MapPath(MapTempFolder), Path.GetFileName(fileName));
                // ...get image and resize it, ...
                var img = new WebImage(fn);
                img.Resize(width, height);
                // ... crop the part the user selected, ...
                img.Crop(top, left, img.Height - top - AvatarStoredHeight, img.Width - left - AvatarStoredWidth);
                // ... delete the temporary file,...
                System.IO.File.Delete(fn);
                // ... and save the new one.
                var newFileName = Path.Combine(AvatarPath, DateTime.Now.ToString("yyyyMMddTHHmmss") + "_" + Path.GetFileName(fn));
                var newFileLocation = HostingEnvironment.MapPath(newFileName);
                if (Directory.Exists(Path.GetDirectoryName(newFileLocation)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newFileLocation));
                }

                img.Save(newFileLocation);
                return new UploadViewModel(true, newFileName, null, origin);
            }
            catch (Exception ex)
            {
                return new UploadViewModel(false, null, "Unable to upload file.\nERRORINFO: " + ex.Message, origin);
            }
        }

        private static bool IsImage(HttpPostedFileBase file)
        {
            if (file == null) return false;
            return file.ContentType.Contains("image") ||
                _imageFileExtensions.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetTempSavedFilePath(HttpPostedFileBase file)
        {
            // Define destination
            var serverPath = HostingEnvironment.MapPath(TempFolder);
            if (Directory.Exists(serverPath) == false)
            {
                Directory.CreateDirectory(serverPath);
            }

            // Generate unique file name
            var fileName = Path.GetFileName(file.FileName);
            fileName = SaveTemporaryAvatarFileImage(file, serverPath, fileName);

            // Clean up old files after every save
            CleanUpTempFolder(1);
            return Path.Combine(TempFolder, fileName);
        }

        private static string SaveTemporaryAvatarFileImage(HttpPostedFileBase file, string serverPath, string fileName)
        {
            var img = new WebImage(file.InputStream);
            var ratio = img.Height / (double)img.Width;
            img.Resize(AvatarScreenWidth, (int)(AvatarScreenWidth * ratio));

            var fullFileName = Path.Combine(serverPath, fileName);
            if (System.IO.File.Exists(fullFileName))
            {
                System.IO.File.Delete(fullFileName);
            }

            img.Save(fullFileName);
            return Path.GetFileName(img.FileName);
        }

        private static void CleanUpTempFolder(int hoursOld)
        {
            try
            {
                var currentUtcNow = DateTime.UtcNow;
                var serverPath = HostingEnvironment.MapPath("/Temp");
                if (!Directory.Exists(serverPath)) return;
                var fileEntries = Directory.GetFiles(serverPath);
                foreach (var fileEntry in fileEntries)
                {
                    var fileCreationTime = System.IO.File.GetCreationTimeUtc(fileEntry);
                    var res = currentUtcNow - fileCreationTime;
                    if (res.TotalHours > hoursOld)
                    {
                        System.IO.File.Delete(fileEntry);
                    }
                }
            }
            catch
            {
            }
        }

    }
}