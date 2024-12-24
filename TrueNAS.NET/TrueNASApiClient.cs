using System.Security.Cryptography;
using System.Text.Json;
using TrueNAS.NET.Models.Connection;
using TrueNAS.NET.Models.Error;
using TrueNAS.NET.Models.Pool;

namespace TrueNAS.NET
{
    /// <summary>
    /// Represents a single TrueNAS connection and exposes all methods in the context of the connection
    /// </summary>
    public class TrueNASApiClient
    {
        private readonly ConnectionInformation _connectionInformation;

        private readonly HttpClient Client;
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
        
        /// <summary>
        /// Creates a new <see cref="TrueNASApiClient"/> object and fetches a connection using an API key
        /// </summary>
        /// <param name="apiKey">API key for authentication with the TrueNAS server</param>
        /// <param name="host">The IP address or domain name, excluding the port number, where the TrueNAS server is hosted</param>
        /// <param name="port">The port number the TrueNAS API is listening on - defaults to 80</param>
        /// <param name="useSecureConnection">Whether or not to use HTTPS for connections</param>
        public TrueNASApiClient(string apiKey, string host, int port = 80, bool useSecureConnection = false)
        {
            // Argument null checks
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey), "API key cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(host)) throw new ArgumentNullException(nameof(host), "Host cannot be null or whitespace.");
            if (port <= 0) throw new ArgumentNullException(nameof(port), "Port cannot be zero.");

            // Argument invalid checks
            if (host.Contains(':')) throw new ArgumentException("Host parameter should not include the port number.", nameof(host));
            
            _connectionInformation = new(apiKey, host, port, useSecureConnection);

            Client = new HttpClient();

            Client.BaseAddress = new Uri($"{(useSecureConnection ? "https" : "http")}://{host}:{port}");
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        #region public methods

        public async Task<List<Pool>> GetPools(int limit = 0, int offset = 0, string sort = null)
        {
            string queryString = "?";

            if (limit > 0) queryString += $"limit={limit}";

            if (offset > 0) queryString += $"&offset={offset}";

            if (sort != null) queryString += $"&sort={sort}";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "api/v2.0/pool" + queryString);

            var result = Client.Send(message);

            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Pool>>(resultContent, serializerOptions);
            }
            else
            {
                switch (result.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new TrueNASAuthorisationException("The server returned a 401 code. Check your API key and try again.");
                    default:
                        throw new HttpRequestException($"The server returned a {result.StatusCode} status with the response body of {await result.Content.ReadAsStringAsync()}");
                }
            }
        }

        #endregion

        #region public properties
        /// <summary>
        /// The port number the TrueNAS API is listening on
        /// </summary>
        public int Port { get { return _connectionInformation.Port; } }

        /// <summary>
        /// The IP or domain name, excluding the port number, where the TrueNAS server is hosted
        /// </summary>
        public string Host { get { return _connectionInformation.Host; } }

        /// <summary>
        /// The full IP or domain name with the port number. Presented in the format 0.0.0.0:0000.
        /// </summary>
        public string FullHost { get { return $"{_connectionInformation.Host}:{_connectionInformation.Port}"; } }

        #endregion

        #region private methods

        #endregion

        #region private properties

        #endregion
    }
}
