using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Error
{
    /// <summary>
    /// The exception that is thrown when the TrueNASApiClient receives a 401 HTTP status code
    /// </summary>
    public class TrueNASAuthorisationException : Exception
    {
        public TrueNASAuthorisationException() { }

        public TrueNASAuthorisationException(string message) : base(message) { }

        public TrueNASAuthorisationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
