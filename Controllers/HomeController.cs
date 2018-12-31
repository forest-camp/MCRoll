using Microsoft.AspNetCore.Mvc;

namespace MCRoll.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "MidWood Camp Roll";

            return View();
        }
    }
}
