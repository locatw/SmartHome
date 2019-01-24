using Microsoft.AspNetCore.Mvc;
using SmartHome.Portal.Domain.Telemetry;
using System.Collections.Generic;

namespace SmartHome.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDeviceRepository deviceRepository;

        public HomeController(IDeviceRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Device> devices = deviceRepository.GetAllAsync().Result;

            return View(devices);
        }
    }
}