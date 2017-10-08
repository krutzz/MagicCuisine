using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models;
using Data.Repository.Contracts;
using Data.UnitOfWork;

namespace Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IUnitOfWork unitOfWork;

        public RecipeService(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork)
        {
            if (recipeRepository == null)
            {
                throw new ArgumentNullException();
            }

            this.recipeRepository = recipeRepository;

            if (unitOfWork == null)
            {
                throw new ArgumentNullException();
            }

            this.unitOfWork = unitOfWork;
        }

        public void CreateRecipe(Recipe recipe)
        {
            this.recipeRepository.Add(recipe);
            this.unitOfWork.Complete();
        }

        public ICollection<Recipe> GetAll(bool isDeleted)
        {
           return this.recipeRepository.Find(r => r.IsDeleted == isDeleted).ToList();
        }
    }
}
