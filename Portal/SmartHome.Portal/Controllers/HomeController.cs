using Microsoft.AspNetCore.Mvc;
using SmartHome.Portal.Domain.Telemetry;
using System;
using System.Linq;

namespace SmartHome.Portal.Controllers
{
    public class HomeController : Controller
    {
        private static readonly TimeZoneInfo Jst = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");

        private ITelemetryRepository telemetryRepository;

        public HomeController(ITelemetryRepository telemetryRepository)
        {
            this.telemetryRepository = telemetryRepository;
        }

        public IActionResult Index()
        {
            var graph = MakeSampleGraph();

            return View(graph);
        }

        private ViewModels.SensorGraph MakeSampleGraph()
        {
            var telemetrySet = telemetryRepository.GetMeasuredTodayAsync("TestDevice1", "Temperature", Jst).Result;
            var labels =
                telemetrySet.Data
                    .Select(telemetry => telemetry.Time)
                    .Select(time => time.ToOffset(Jst.BaseUtcOffset).ToString("HH:mm:ss"));
            var data = telemetrySet.Data.Select(telemetry => telemetry.DoubleValue);

            return new ViewModels.SensorGraph { Title = "室温", Unit = "℃", MaxValue = 40, Labels = labels, Data = data };
        }
    }
}