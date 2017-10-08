using AutoMapper;
using Data.Models;
using MagicCuisine.Areas.Admin.Models;
using Services.Contracts;
using System.Web.Mvc;

namespace MagicCuisine.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
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
            var recipe = Mapper.Map<Recipe>(model);
            this.recipeService.CreateRecipe(recipe);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}