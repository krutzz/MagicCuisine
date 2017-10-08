using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Address
    {
        public Address()
        {
            this.ID = Guid.NewGuid();
        }

        public Address(string street, string building, string entrance, string floor, string flat, string postalCode, Country country, Town town)
            : this()
        {
            this.Street = street;
            this.Building = building;
            this.Entrance = entrance;
            this.Floor = floor;
            this.Flat = flat;
            this.PostalCode = postalCode;
            this.Country = country;
            this.Town = town;
        }

        public Guid ID { get; set; }

        [Required]
        [MaxLength(150)]
        public string Street { get; set; }

        [Required]
        [MaxLength(10)]
        public string Building { get; set; }

        [Required]
        [MaxLength(10)]
        public string Entrance { get; set; }

        [Required]
        [MaxLength(10)]
        public string Floor { get; set; }

        [Required]
        [MaxLength(10)]
        public string Flat { get; set; }

        [Required]
        [MaxLength(10)]
        public string PostalCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual Town Town { get; set; }

    }
}
