using AutoMapper;
using MagicCuisine.Models;
using MagicCuisine.Providers;
using Services.Contracts;
using System;
using System.Web.Mvc;

namespace MagicCuisine.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IMapProvider mapProvider;

        public RecipeController(IRecipeService recipeService, IMapProvider mapProvider)
        {
            if (recipeService == null)
            {
                throw new ArgumentNullException();
            }

            this.recipeService = recipeService;

            if (mapProvider == null)
            {
                throw new ArgumentNullException();
            }

            this.mapProvider = mapProvider;
        }

        // GET: Recipe
        public ActionResult Index(Guid id)
        {
            var recipe = this.recipeService.GetById(id);
            
            var recipeModel = this.mapProvider.GetMap<RecipeViewModel>(recipe);

            var model = new RecipeIndexViewModel()
            {
                Recipe = recipeModel
            };

            return View(model);
        }
    }
}