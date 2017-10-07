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
            this.success = success;
            this.avatarFileLocation = avatarFileLocation;
            this.errorMessage = errorMessage;
            this.origin = origin;
        }

        public bool success { get; set; }
        public string avatarFileLocation { get; set; }
        public string errorMessage { get; set; }
        public string origin { get; set; }


        public object ToJson()
        {
            if (this.success)
            {
                return new { success = true, avatarFileLocation = this.avatarFileLocation, origin = this.origin };
            }
            else
            {
                return new { success = false, errorMessage = this.errorMessage, origin = this.origin };
            }
        }
    }
}