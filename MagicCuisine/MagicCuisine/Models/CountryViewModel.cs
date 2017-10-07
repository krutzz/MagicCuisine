using Data.Models;
using MagicCuisine.Infrastructure;
using System;

namespace MagicCuisine.Models
{
    public class CountryViewModel : IMapFrom<Country>
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
    }
}