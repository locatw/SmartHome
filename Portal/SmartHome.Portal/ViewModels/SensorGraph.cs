using System.Collections.Generic;

namespace SmartHome.Portal.ViewModels
{
    public class SensorGraph
    {
        public string Title { get; set; }

        public string Unit { get; set; }
        
        public double MaxValue { get; set; }

        public IEnumerable<string> Labels { get; set; }

        public IEnumerable<double> Data { get; set; }
    }
}
