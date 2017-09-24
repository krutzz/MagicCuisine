using MagicCuisine.Models;
using System.Collections.Generic;
using System.Web;

namespace MagicCuisine.Helpers.Contracts
{
    public interface IFileHelper
    {
        UploadViewModel UploadFile(IEnumerable<HttpPostedFileBase> files);

        UploadViewModel CropImage(string t, string l, string h, string w, string fileName);
    }
}