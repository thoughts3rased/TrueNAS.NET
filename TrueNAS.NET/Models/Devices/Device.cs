using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Devices
{
    public class Device
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }

        public string Status { get; set; }

        public DeviceStats Stats { get; set; }

        public List<Device> Children { get; set; }

        [JsonPropertyName("device")]
        public string UnixDeviceName { get; set; }

        public string Disk { get; set; }

        [JsonPropertyName("unavail_disk")]
        public string UnavailableDisk { get; set; }
    }
}
