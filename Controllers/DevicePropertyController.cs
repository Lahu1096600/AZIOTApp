using AZIotApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AZIotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicePropertyController : ControllerBase
    {
        private readonly IIOTDeviceProperty _iotDeviceProperty;
        public DevicePropertyController(IIOTDeviceProperty iotDeviceProperty)
        {
            _iotDeviceProperty = iotDeviceProperty;
        }

        [HttpPost]
        [Route("UpdateDeviceProperty")]
        public IActionResult UpdateDeviceProperty(IOTReportedProperties iOTReportedProperties)
        {
            return Ok(_iotDeviceProperty.UpdateDeviceProperties(iOTReportedProperties));
        }

        [HttpPost]
        [Route("UpdateDesiredProperties")]
        public IActionResult UpdateDesiredProperties(string deviceName)
        {
            return Ok(_iotDeviceProperty.UpdateDesiredProperties(deviceName));
        }

        [HttpPost]
        [Route("SendTelemetryMessage")]
        public void SendTelemetryMessage()
        {
            _iotDeviceProperty.SendDeviceToCloudMessagesAsync();
        }
    }
}
