using System;

namespace SmartHome.Portal.Domain.Telemetry
{
    public interface ITelemetry
    {
        DateTimeOffset Time { get; }

        double DoubleValue { get; }
    }
}
