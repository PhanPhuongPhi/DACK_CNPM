using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var model = new Cart();
            return View(model);
        }
    }
}
