using Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagicCuisine.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public ICollection<Country> Countries { get; set; }

        public int Country { get; set; }

        public ICollection<Country> Towns { get; set; }

        public int Town { get; set; }

        public string Street { get; set; }

        public string Flat { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Building { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

    }
}