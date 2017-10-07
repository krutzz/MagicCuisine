using Data.Models;
using MagicCuisine.Infrastructure;

namespace MagicCuisine.Areas.Admin.Models
{
    public class IndexViewModel : IMapFrom<Recipe>
    {
        public string Avatar { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}