using Data.Models;
using MagicCuisine.Infrastructure;
using System;

namespace MagicCuisine.Models
{
    public class RecipeViewModel : IMapFrom<Recipe>
    {
        public Guid ID { get; set; }

        public bool IsDeleted { get; set; }

        public string Avatar { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}