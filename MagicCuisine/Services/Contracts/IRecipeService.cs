using Data.Models;

namespace Services.Contracts
{
    public interface IRecipeService
    {
        void CreateRecipe(Recipe recipe);
    }
}
