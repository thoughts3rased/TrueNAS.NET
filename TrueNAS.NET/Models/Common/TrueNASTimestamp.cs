using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Common
{
    public class TrueNASTimestamp
    {
        [JsonPropertyName("$date")]
        private int Date { get; set; }

        [JsonIgnore]
        public int UnixTime { get { return Date; } }

        [JsonIgnore]
        public DateTime Time { get { return DateTimeOffset.FromUnixTimeSeconds(Date).DateTime; } }
    }
}
