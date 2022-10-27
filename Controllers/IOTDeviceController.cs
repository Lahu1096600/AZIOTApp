using AZIotApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;

namespace AZIotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IOTDeviceController : ControllerBase
    {
        private readonly IIotDeviceCRUD _iotDeviceCRUD;

        public IOTDeviceController(IIotDeviceCRUD iotDeviceCRUD)
        {
            _iotDeviceCRUD = iotDeviceCRUD;
        }

        [HttpGet]
        [Route("AddDevice")]
        public Task<Device> AddDeviceAsync(string deviceId)
        {
            return _iotDeviceCRUD.AddDeviceAsync(deviceId);
        }


        [HttpGet]
        [Route("GetDevice")]
        public Task<Device> GetDeviceAsync(string deviceId)
        {
            return _iotDeviceCRUD.GetDeviceAsync(deviceId);
        }

        [HttpGet]
        [Route("RemoveDevice")]
        public IActionResult RemoveDeviceAsync(string deviceId)
        {
            return Ok(_iotDeviceCRUD.RemoveDeviceAsync(deviceId));
        }


        [HttpPut]
        [Route("UpdateDevice")]
        public IActionResult UpdateDeviceAsync(string deviceId)
        {
            return Ok(_iotDeviceCRUD.UpdateDeviceAsync(deviceId));
        }

    }
}
