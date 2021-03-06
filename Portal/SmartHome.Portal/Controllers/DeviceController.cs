﻿using Microsoft.AspNetCore.Mvc;
using SmartHome.Portal.Domain.Telemetry;
using System.Collections.Generic;

namespace SmartHome.Portal.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository deviceRepository;

        public DeviceController(IDeviceRepository deviceRepository)
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