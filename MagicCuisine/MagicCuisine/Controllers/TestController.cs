using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicCuisine.Controllers
{
    public class TestController : Controller
    {
        private readonly IDataBase db;

        public TestController(IDataBase db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}