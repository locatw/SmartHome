using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Portal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}