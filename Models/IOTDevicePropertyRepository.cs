using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System.Text;
using Message = Microsoft.Azure.Devices.Client.Message;

namespace AZIotApp.Models
{
    public class IOTDevicePropertyRepository : IIOTDeviceProperty
    {
        static RegistryManager registryManager;
        static DeviceClient deviceClient;
        static string iotconnectionString = "HostName=MyFirstIOTHub3009.azure-devices.net;DeviceId=MyFirstDevice;SharedAccessKey=rZqhJk0DYAaGVU/dJ8fYD5U9cZtu+sOnDw2vndugJyk=";

        private readonly static string s_connectionString01 = "HostName=MyFirstIOTHub0310.azure-devices.net;DeviceId=MyfirstDevice0310;SharedAccessKey=OYG3kOnCOtjjLWddPMa6GKdBkyov0Trl6Pt+WmqXTLI=";

        public async Task UpdateDeviceProperties(IOTReportedProperties iOTReportedProperties)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(iotconnectionString);
            TwinCollection reportedProperties, connectivity, temperature;
            reportedProperties = new TwinCollection();
            connectivity = new TwinCollection();
            temperature = new TwinCollection();

            connectivity["type"] = "Cellular";
            temperature["HighTemp"] = "27";

            reportedProperties["connectivity"] = connectivity;
            reportedProperties["temparature"] = temperature;
            reportedProperties["sensortype"] = iOTReportedProperties.SensorType;
            await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);

        }

        public async Task UpdateDesiredProperties(string deviceName)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(iotconnectionString);
            var client = await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredProperties, telemetryConfig;
            desiredProperties = new TwinCollection();
            telemetryConfig = new TwinCollection();

            telemetryConfig["frequency"] = "5Hz";

            desiredProperties["telemetryconfig"] = telemetryConfig;
            client.Properties.Desired["telemetryconfig"] = telemetryConfig;

            await registryManager.UpdateTwinAsync(client.DeviceId, client, client.ETag);

        }

        public async void SendDeviceToCloudMessagesAsync()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString01, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            try
            {
                double minTemperature = 20;
                double minHumidity = 60;
                Random rand = new Random();

                while (true)
                {
                    double currentTemperature = minTemperature + rand.NextDouble() * 15;
                    double currentHumidity = minHumidity + rand.NextDouble() * 20;

                    // Create JSON message  

                    var telemetryDataPoint = new
                    {

                        temperature = currentTemperature,
                        humidity = currentHumidity
                    };

                    string messageString = "";



                    messageString = JsonConvert.SerializeObject(telemetryDataPoint);

                    var message = new Message(Encoding.ASCII.GetBytes(messageString));


                    // Send the telemetry message  
                    await deviceClient.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                    await Task.Delay(1000 * 10);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
