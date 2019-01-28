using System.Collections.Generic;

namespace SmartHome.Portal.Domain.Telemetry
{
    public class TelemetrySet
    {
        public TelemetrySet(string deviceId, IEnumerable<ITelemetry> telemetries)
        {
            DeviceId = deviceId;
            Data = telemetries;
        }

        public string DeviceId { get; }

        public IEnumerable<ITelemetry> Data { get; }
    }
}
