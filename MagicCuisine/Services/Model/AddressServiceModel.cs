using System;

namespace Services.Model
{
    public class AddressServiceModel
    {
        public string Street { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Flat { get; set; }

        public string PostalCode { get; set; }

        public Guid Country { get; set; }

        public Guid Town { get; set; }
    }
}
