using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Street { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Flat { get; set; }

        public string PostalCode { get; set; }

        public virtual Country Country { get; set; }

        public virtual Town Town { get; set; }

    }
}
