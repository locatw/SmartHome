using Google.Cloud.Datastore.V1;
using SmartHome.Portal.Domain.Telemetry;
using System;

namespace SmartHome.Portal.Infra.Telemetry
{
    public class Telemetry : ITelemetry
    {
        private Value value;

        public Telemetry(DateTimeOffset time, Value value)
        {
            Time = time;
            this.value = value;
        }

        public DateTimeOffset Time { get; }

        public double DoubleValue { get { return value.DoubleValue; } }
    }
}
