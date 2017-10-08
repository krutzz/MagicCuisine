using AutoMapper;
using MagicCuisine.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicCuisine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService recipeService;

        public HomeController(IRecipeService recipeService)
        {
            if (recipeService == null)
            {
                throw new ArgumentNullException();
            }

            this.recipeService = recipeService;
        }

        public ActionResult Index()
        {
            var recipes = this.recipeService.GetAll(false)
                                            .Select(r => Mapper.Map<RecipeViewModel>(r))
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