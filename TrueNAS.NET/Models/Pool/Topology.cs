using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNAS.NET.Models.Devices;

namespace TrueNAS.NET.Models.Pool
{
    public class Topology
    {
        public List<Device> Data { get; set; }

        public List<Device> Log { get; set; }

        public List<Device> Cache { get; set; }

        public List<Device> Spare { get; set; }

        public List<Device> Special { get; set; }

        public List<Device> Dedup { get; set; }
    }
}
