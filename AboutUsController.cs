using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            var model = new AboutUs();
            return View(model);
        }
    }
}
