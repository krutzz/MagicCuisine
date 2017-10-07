using Data.Models;
using MagicCuisine.Infrastructure;
using System;

namespace MagicCuisine.Models
{
    public class TownViewModel : IMapFrom<Town>
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public CountryViewModel Country { get; set; }
    }
}