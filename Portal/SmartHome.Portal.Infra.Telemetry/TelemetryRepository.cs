using Google.Cloud.Datastore.V1;
using SmartHome.Portal.Domain.Telemetry;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Portal.Infra.Telemetry
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private static readonly string EntityKind = "Telemetry";

        private static readonly string DeviceIdPropertyName = "DeviceID";

        private static readonly string TimePropertyName = "Time";

        private readonly string projectId;

        private DatastoreDb db;

        private KeyFactory keyFactory;

        public TelemetryRepository(string projectId)
        {
            this.projectId = projectId;
            db = DatastoreDb.Create(projectId);
            keyFactory = db.CreateKeyFactory(EntityKind);
        }

        public async Task<TelemetrySet> GetMeasuredTodayAsync(string deviceId, string telemetryKey, TimeZoneInfo timeZone)
        {
            var utcToday = DateTimeOffset.UtcNow;
            var localToday = utcToday.ToOffset(timeZone.BaseUtcOffset);
            var localTodayStart = new DateTimeOffset(localToday.Year, localToday.Month, localToday.Day, 0, 0, 0, localToday.Offset);
            var localNextDayStart = localTodayStart.AddDays(1);

            var query = new Query(EntityKind)
            {
                Filter =
                    Filter.And(
                        Filter.Equal(DeviceIdPropertyName, deviceId),
                        Filter.GreaterThanOrEqual(TimePropertyName, localTodayStart.UtcTicks),
                        Filter.LessThan(TimePropertyName, localNextDayStart.UtcTicks)),
                Order = { { TimePropertyName, PropertyOrder.Types.Direction.Ascending } }
            };

            var queryResult = await db.RunQueryAsync(query);
            var data =
                queryResult.Entities.Select(entity =>
                {
                    var time = new DateTimeOffset(entity[TimePropertyName].IntegerValue, new TimeSpan());

                    return new Telemetry(time, entity[telemetryKey]);
                });

            return new TelemetrySet(deviceId, data);
        }
    }
}
