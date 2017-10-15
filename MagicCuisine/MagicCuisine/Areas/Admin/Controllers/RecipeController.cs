using Data.Models;
using MagicCuisine.Areas.Admin.Models;
using MagicCuisine.Providers;
using Services.Contracts;
using System;
using System.Web.Mvc;

namespace MagicCuisine.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        // GET: Admin/Recipе
        [HttpGet]
        public ActionResult Index()
        {
            var avatar = TempData["avatar"] ?? "/Avatars/img-default.png";

            var model = new IndexViewModel()
            {
                Avatar = (string)avatar
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IndexViewModel model)
        {
            var recipe = this.mapProvider.GetMap<Recipe>(model);
            this.recipeService.CreateRecipe(recipe);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}