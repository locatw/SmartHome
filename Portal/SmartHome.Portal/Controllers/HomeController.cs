using Microsoft.AspNetCore.Mvc;
using System;

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