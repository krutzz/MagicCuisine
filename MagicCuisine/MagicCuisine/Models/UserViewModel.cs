﻿using Data.Models;
using MagicCuisine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicCuisine.Models
{
    public class UserViewModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public Guid? AddressId { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }
    }
}