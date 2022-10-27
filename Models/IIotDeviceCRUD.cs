using Microsoft.Azure.Devices;

namespace AZIotApp.Models
{
    public interface IIotDeviceCRUD
    {
        Task<Device> AddDeviceAsync(string deviceId);
        Task<Device> GetDeviceAsync(string deviceId);
        Task RemoveDeviceAsync(string deviceId);
        Task UpdateDeviceAsync(string deviceId);
    }
}
