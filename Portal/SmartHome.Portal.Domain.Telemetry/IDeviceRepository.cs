using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Portal.Domain.Telemetry
{
    public interface IDeviceRepository
    {
        Task<IList<Device>> GetAllAsync();
    }
}
