using Google.Apis.Auth.OAuth2;
using Google.Apis.CloudIot.v1;
using Google.Apis.Services;
using SmartHome.Portal.Domain.Telemetry;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Portal.Infra.Telemetry
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly string projectId;

        private readonly string region;

        private readonly string registryId;

        private CloudIotService cloudIotClient;

        public DeviceRepository(string projectId, string region, string registryId)
        {
            this.projectId = projectId;
            this.region = region;
            this.registryId = registryId;
            cloudIotClient = CreateAuthorizedClient();
        }

        public async Task<IList<Device>> GetAllAsync()
        {
            string parent = $"projects/{projectId}/locations/{region}/registries/{registryId}";
            var deviceListResult = await cloudIotClient.Projects.Locations.Registries.Devices.List(parent).ExecuteAsync();

            var devices = deviceListResult.Devices.Select(async device =>
            {
                string name = $"{parent}/devices/{device.Id}";
                Google.Apis.CloudIot.v1.Data.Device deviceInfo =
                    await cloudIotClient.Projects.Locations.Registries.Devices.Get(name).ExecuteAsync();
                IDictionary<string, string> metadata = deviceInfo.Metadata;

                return new Device { ID = device.Id, Name = metadata["Name"] };
            });

            return await Task.WhenAll(devices);
        }

        private CloudIotService CreateAuthorizedClient()
        {
            var credential = GoogleCredential.GetApplicationDefault();
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    CloudIotService.Scope.CloudPlatform
                });
            }

            return new CloudIotService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                GZipEnabled = false
            });
        }
    }
}
