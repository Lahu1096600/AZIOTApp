using Microsoft.Azure.Devices;

namespace AZIotApp.Models
{
    public class IOTDeviceCRUDRepository : IIotDeviceCRUD
    {
        static RegistryManager registryManager;
        static Device device;
        static string iotconnectionString = "HostName=MyFirstIOTHub0310.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=6yFQTnVAE8Kj4MA3oXfjTMu3HPr9bevZqMOlW4Ex4Xs=";

        public async Task<Device> AddDeviceAsync(string deviceId)
        {
            registryManager = RegistryManager.CreateFromConnectionString(iotconnectionString);
            device = await registryManager.AddDeviceAsync(new Device(deviceId));
            return device;
        }


        public async Task<Device> GetDeviceAsync(string deviceId)
        {
            registryManager = RegistryManager.CreateFromConnectionString(iotconnectionString);
            device = await registryManager.GetDeviceAsync(deviceId);
            return device;
        }

        public async Task RemoveDeviceAsync(string deviceId)
        {
            registryManager = RegistryManager.CreateFromConnectionString(iotconnectionString);
            await registryManager.RemoveDeviceAsync(deviceId);

        }

        public async Task UpdateDeviceAsync(string deviceId)
        {
            Device devicetoUpdate;
            registryManager = RegistryManager.CreateFromConnectionString(iotconnectionString);
            try
            {
                devicetoUpdate = await registryManager.GetDeviceAsync(deviceId);
                devicetoUpdate.Status = Microsoft.Azure.Devices.DeviceStatus.Enabled;
                await registryManager.UpdateDeviceAsync(devicetoUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
