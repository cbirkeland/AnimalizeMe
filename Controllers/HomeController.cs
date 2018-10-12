using Microsoft.AspNetCore.Mvc;

namespace AnimalizeMe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Restaurant");
        }

    }
}
