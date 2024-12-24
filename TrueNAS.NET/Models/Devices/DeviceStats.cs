using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Devices
{
    public class DeviceStats
    {
        public long Timestamp { get; set; }

        public int ReadErrors { get; set; }

        public int WriteErrors { get; set; }

        public int ChecksumErrors { get; set; }

        public List<int> Ops { get; set; }

        public List<long> Bytes { get; set; }

        public long Size { get; set; }

        public long Allocated { get; set; }

        public long Fragmentation { get; set; }

        public int SelfHealed { get; set; }

        public int ConfiguredAshift { get; set; }

        public int LogicalAshift { get; set; }

        public int PhysicalAshift { get; set; }
    }
}
