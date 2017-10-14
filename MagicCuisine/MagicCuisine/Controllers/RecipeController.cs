using AutoMapper;
using MagicCuisine.Models;
using Services.Contracts;
using System;
using System.Web.Mvc;

namespace MagicCuisine.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            if(recipeService == null)
            {
                throw new ArgumentNullException();
            }

            this.recipeService = recipeService;
        }

        // GET: Recipe
        public ActionResult Index(Guid id)
        {
            var recipe = this.recipeService.GetById(id);
            var recipeModel = Mapper.Map<RecipeViewModel>(recipe);

            var model = new RecipeIndexViewModel()
            {
                Recipe = recipeModel
            };

            return View(model);
        }
    }
}