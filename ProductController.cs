using Microsoft.AspNetCore.Mvc;

namespace Web_app_project.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Laptops()
        {
            return View();
        }

        public IActionResult Phones()
        {
            return View();
        }

        public IActionResult Headsets()
        {
            return View();
        }

        public IActionResult Mouse()
        {
            return View();
        }
    }
}
