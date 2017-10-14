using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicCuisine.Models
{
    public class UploadViewModel
    {
        public UploadViewModel(bool success, string avatarFileLocation, string errorMessage, string origin)
        {
            this.Success = success;
            this.AvatarFileLocation = avatarFileLocation;
            this.ErrorMessage = errorMessage;
            this.Origin = origin;
        }

        public bool Success { get; set; }
        public string AvatarFileLocation { get; set; }
        public string ErrorMessage { get; set; }
        public string Origin { get; set; }


        public object ToJson()
        {
            if (this.Success)
            {
                return new { success = true, avatarFileLocation = this.AvatarFileLocation, origin = this.Origin };
            }
            else
            {
                return new { success = false, errorMessage = this.ErrorMessage, origin = this.Origin };
            }
        }
    }
}