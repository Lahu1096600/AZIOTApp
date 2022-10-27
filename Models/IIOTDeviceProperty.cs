namespace AZIotApp.Models
{
    public interface IIOTDeviceProperty
    {
        Task UpdateDeviceProperties(IOTReportedProperties iOTReportedProperties);
        Task UpdateDesiredProperties(string deviceName);
        void SendDeviceToCloudMessagesAsync();
    }
}
