using System.Collections.Generic;

namespace MagicCuisine.Models
{
    public class HomeIndexViewModel
    {
        public string LogoImg { get; set; }

        public IList<RecipeViewModel> Recipes { get; set; }
    }
}