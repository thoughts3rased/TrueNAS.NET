using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Pool
{
    public class Pool
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Guid { get; set; }

        public string Path { get; set; }

        public string Status { get; set; }

        public Scan Scan { get; set; }

        public Expand Expand { get; set; }

        public bool IsUpgraded { get; set; }

        public Topology Topology { get; set; }

        public bool Healthy { get; set; }

        public bool Warning { get; set; }

        public string StatusCode { get; set; }

        public string StatusDetail { get; set; }

        public long Size { get; set; }

        public long Allocated { get; set; }

        public long Free { get; set; }

        public long Freeing { get; set; }

        public string Fragmentation { get; set; }

        public AutoTrim AutoTrim { get; set; }
    }
}
