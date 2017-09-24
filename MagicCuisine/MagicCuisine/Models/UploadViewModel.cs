using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicCuisine.Models
{
    public class UploadViewModel
    {
        public UploadViewModel(bool success, string avatarFileLocation, string errorMessage)
        {
            this.success = success;
            this.avatarFileLocation = avatarFileLocation;
            this.errorMessage = errorMessage;
        }

        public bool success { get; set; }
        public string avatarFileLocation { get; set; }
        public string errorMessage { get; set; }

        public object ToJson()
        {
            if (this.success)
            {
                return new { success = true, avatarFileLocation = this.avatarFileLocation };
            }
            else
            {
                return new { success = false, errorMessage = this.errorMessage };
            }
        }
    }
}