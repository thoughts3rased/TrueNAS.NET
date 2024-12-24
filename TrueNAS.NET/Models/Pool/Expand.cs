using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Pool
{
    public class Expand
    {
        public string State { get; set; }

        public int ExpandingVDev { get; set; }

        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public long BytesToReflow { get; set; }

        public long BytesReflowed { get; set; }

        public int WaitingForResilver { get; set; }

        public long TotalSecsLeft { get; set; }

        public Decimal Percentage { get; set; }
    }
}
