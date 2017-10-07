using Data.Models;
using MagicCuisine.Infrastructure;

namespace MagicCuisine.Models
{
    public class AddressViewModel : IMapFrom<Address>
    {
        public string Street { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Flat { get; set; }

        public string PostalCode { get; set; }

        public Country Country { get; set; }

        public Town Town { get; set; }
    }
}