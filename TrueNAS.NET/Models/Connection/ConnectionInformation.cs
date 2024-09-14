using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Connection
{
    public class ConnectionInformation
    {
        public ConnectionInformation(string apiKey, string host, int port)
        {
            ApiKey = apiKey;
            Host = host;
            Port = port;
        }

        /// <summary>
        /// API key for authenticating with the TrueNAS API
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// IP or FQDN where the instance of TrueNAS is located, excluding the port
        /// </summary>
        public string Host {  get; set; }

        /// <summary>
        /// Port that the TrueNAS API is listening on
        /// </summary>
        public int Port { get; set; }
    }
}
