using Data.Contracts;
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