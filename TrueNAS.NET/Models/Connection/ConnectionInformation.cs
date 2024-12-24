using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNAS.NET.Models.Connection
{
    public class ConnectionInformation(string apiKey, string host, int port, bool secureConnection)
    {

        /// <summary>
        /// API key for authenticating with the TrueNAS API
        /// </summary>
        public string ApiKey { get; set; } = apiKey;

        /// <summary>
        /// IP or FQDN where the instance of TrueNAS is located, excluding the port
        /// </summary>
        public string Host { get; set; } = host;

        /// <summary>
        /// Port that the TrueNAS API is listening on
        /// </summary>
        public int Port { get; set; } = port;

        /// <summary>
        /// Whether or not the connection is using HTTPS
        /// </summary>
        public bool SecureConnection { get; set; } = secureConnection;
    }
}
