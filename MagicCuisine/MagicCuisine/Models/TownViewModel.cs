using Data.Models;
using MagicCuisine.Infrastructure;

namespace MagicCuisine.Models
{
    public class TownViewModel : IMapFrom<Town>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public CountryViewModel Country { get; set; }
    }
}