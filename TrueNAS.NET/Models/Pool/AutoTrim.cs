using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Pool
{
    public class AutoTrim
    {
        public string Value { get; set; }

        public string RawValue { get; set; }

        public string Parsed { get; set; }

        public string Source { get; set; }
    }
}
