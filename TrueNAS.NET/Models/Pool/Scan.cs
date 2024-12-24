using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TrueNAS.NET.Models.Common;

namespace TrueNAS.NET.Models.Pool
{
    public class Scan
    {
        public string Function { get; set; }

        public string State { get; set; }

        public TrueNASTimestamp StartTime { get; set; }

        public TrueNASTimestamp EndTime { get; set; }

        public Decimal Percentage { get; set; }

        public long BytesToProcess { get; set; }

        public long BytesProcessed { get; set; }

        public long BytesIssued { get; set; }

        public string Pause { get; set; }

        public int Errors { get; set; }

        public long TotalSecondsLeft { get; set; }
    }
}
