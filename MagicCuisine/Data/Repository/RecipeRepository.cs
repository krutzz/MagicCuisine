using Data.Models;
using Data.Repository.Contracts;

namespace Data.Repository
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(CuisineDbContext context)
            : base(context)
        {
        }
    }
}
