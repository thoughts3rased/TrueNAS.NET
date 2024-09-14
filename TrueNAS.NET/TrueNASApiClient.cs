using TrueNAS.NET.Models.Connection;

namespace TrueNAS.NET
{
    /// <summary>
    /// Represents a single TrueNAS connection, with all the methods available for the particular 
    /// </summary>
    public class TrueNASApiClient
    {
        private ConnectionInformation _connectionInformation;
        
        public TrueNASApiClient(string apiKey, string host, int port)
        {
            _connectionInformation = new(apiKey, host, port);
        }
    }
}
