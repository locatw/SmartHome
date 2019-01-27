using Google.Cloud.Datastore.V1;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceEmurator
{
    class Program
    {
        private static readonly string ProjectIdEnvironmentKey = "PROJECT_ID";

        private static readonly string GapEnvironmentKey = "GOOGLE_APPLICATION_CREDENTIALS";

        private static readonly string TelemetryEntityKind = "Telemetry";

        private static readonly string ProjectId;

        private static readonly TimeZoneInfo jst = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        private DatastoreDb db;

        private KeyFactory keyFactory;

        private Random random = new Random();

        private readonly string deviceId;

        static Program()
        {
            CheckEnvironmentVariableExistence(ProjectIdEnvironmentKey);

            ProjectId = Environment.GetEnvironmentVariable(ProjectIdEnvironmentKey);
        }

        public Program(string deviceId)
        {
            this.deviceId = deviceId;
        }

        static async Task Main(string[] args)
        {
            CheckEnvironmentVariableExistence(GapEnvironmentKey);

            if (args.Length != 1)
            {
                Console.WriteLine("device id is required in arguments.");
                Console.WriteLine("Usage: dotnet run [device id]");
                return;
            }

            await (new Program(args[0])).RunAsync();
        }

        private async Task RunAsync()
        {
            db = DatastoreDb.Create(ProjectId);
            keyFactory = db.CreateKeyFactory(TelemetryEntityKind);

            var now = DateTimeOffset.Now;
            int count = 12;

            var times =
                Enumerable
                    .Range(0, count)
                    .Select(i => new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, i * 5, 0, now.Offset));
            var temperatures = times.Select(time => 15.0 + 5.0 * (random.NextDouble() - 0.5));
            var entities =
                times
                    .Zip(temperatures, (time, temperature) => new { Time = time, Temperature = temperature })
                    .Select(data => CreateEntity(data.Time, deviceId, data.Temperature));

            await db.InsertAsync(entities);
        }

        private Entity CreateEntity(DateTimeOffset time, string deviceId, double temperature)
        {
            return new Entity()
            {
                Key = keyFactory.CreateIncompleteKey(),
                ["Time"] = time,
                ["DeviceID"] = deviceId,
                ["Temperature"] = temperature
            };
        }

        private static void CheckEnvironmentVariableExistence(string key)
        {
            if (!Environment.GetEnvironmentVariables().Contains(key))
            {
                throw new Exception($"environment variable {key} does not exist.");
            }
        }
    }
}
