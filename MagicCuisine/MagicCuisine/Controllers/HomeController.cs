using MagicCuisine.Models;
using MagicCuisine.Providers;
using Services.Contracts;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MagicCuisine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IMapProvider mapProvider;

        public HomeController(IRecipeService recipeService, IMapProvider mapProvider)
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

        public ActionResult Index()
        {
            var recipes = this.recipeService.GetAll(false)
                                            .Select(r => this.mapProvider.GetMap<RecipeViewModel>(r))
                                            .ToList();

            var model = new HomeIndexViewModel()
            {
                LogoImg = "Content/Images/general/logo.png",
                Recipes = recipes
            };

            return View(model);
        }
    }
}