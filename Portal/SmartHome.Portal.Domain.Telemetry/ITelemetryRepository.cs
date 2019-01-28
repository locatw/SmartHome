using System;
using System.Threading.Tasks;

namespace SmartHome.Portal.Domain.Telemetry
{
    public interface ITelemetryRepository
    {
        Task<TelemetrySet> GetMeasuredTodayAsync(string deviceId, string telemetryKey, TimeZoneInfo timeZone);
    }
}
