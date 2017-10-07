using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace MagicCuisine.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public UserViewModel User { get; set; }

        public AddressViewModel Address { get; set; }
    }


}