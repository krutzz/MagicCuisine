using Data.Models;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IRecipeService
    {
        void CreateRecipe(Recipe recipe);

        ICollection<Recipe> GetAll(bool isDeleted);
    }
}
