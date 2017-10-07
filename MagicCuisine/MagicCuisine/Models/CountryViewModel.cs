using Data.Models;
using MagicCuisine.Infrastructure;

namespace MagicCuisine.Models
{
    public class CountryViewModel : IMapFrom<Country>
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}