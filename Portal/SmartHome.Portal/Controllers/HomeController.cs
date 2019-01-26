using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SmartHome.Portal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var graph = MakeSampleGraph();

            return View(graph);
        }

        private ViewModels.SensorGraph MakeSampleGraph()
        {
            var jst = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
            var now = DateTimeOffset.Now;

            var random = new Random();
            var count = 12;
            var labels =
                Enumerable
                    .Range(0, count)
                    .Select(i => new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, i * 5, 0, now.Offset))
                    .Select(date => date.ToOffset(jst.BaseUtcOffset).ToString("HH:mm:ss"));
            var data = Enumerable.Range(0, count).Select(_ => 15.0 + 5.0 * (random.NextDouble() - 0.5));

            return new ViewModels.SensorGraph { Title = "室温", Unit = "℃", MaxValue = 40, Labels = labels, Data = data };
        }
    }
}