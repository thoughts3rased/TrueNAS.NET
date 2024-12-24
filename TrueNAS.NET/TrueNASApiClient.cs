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

        /// <summary>
        /// Fetches a list of ZFS pools that currently exist on the server
        /// </summary>
        /// <param name="limit">How many items to fetch</param>
        /// <param name="offset">How many items to offset the result set by</param>
        /// <param name="sort">The property to sort the data by</param>
        /// <returns>A list of Pools and their corresponding data</returns>
        public async Task<List<Pool>> GetPools(int limit = 0, int offset = 0, string sort = null)
        {
            string queryString = "?";

            if (limit > 0) queryString += $"limit={limit}";

            if (offset > 0) queryString += $"&offset={offset}";

            if (sort != null) queryString += $"&sort={sort}";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "api/v2.0/pool" + queryString);

            HttpResponseMessage result = Client.Send(message);

            if (result.IsSuccessStatusCode)
            {
                string resultContent = await result.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Pool>>(resultContent, serializerOptions);
            }
            else
            {
                throw await HandleUnsuccessfulResponse(result);
            }
        }

        /// <summary>
        /// Returns a count of the number of pools that exist on the server
        /// </summary>
        /// <returns>The number of pools that exist on the server</returns>
        public async Task<int> GetPoolCount()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "api/v2.0/pool?count=true");

            HttpResponseMessage result = Client.Send(message);

            if (result.IsSuccessStatusCode)
            {
                string responseString = await result.Content.ReadAsStringAsync();

                return int.Parse(responseString);
            }
            else
            {
                throw await HandleUnsuccessfulResponse(result);
            }
        }

        /// <summary>
        /// Fetches a pool for a given pool ID
        /// </summary>
        /// <param name="id">The id of the target pool to fetch</param>
        /// <returns>The requested pool, if found</returns>
        public async Task<Pool> GetPool(int id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"api/v2.0/pool/id/{id}");

            HttpResponseMessage result = Client.Send(message);

            if (result.IsSuccessStatusCode)
            {
                string resultContent = await result.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<Pool>(resultContent, serializerOptions);
            }
            else
            {      
                throw await HandleUnsuccessfulResponse(result);
            }
        }

        /// <summary>
        /// Fetches a pool for a given pool name
        /// </summary>
        /// <param name="poolName">The name of the pool to fetch</param>
        /// <returns>The requested pool, if found</returns>
        public async Task<Pool> GetPool(string poolName)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, $"api/v2.0/pool/get_instance_by_name");

            message.Content = new StringContent($"\"{poolName}\"");

            HttpResponseMessage result = Client.Send(message);

            if (result.IsSuccessStatusCode)
            {
                string resultContent = await result.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<Pool>(resultContent, serializerOptions);
            }
            else
            {
                throw await HandleUnsuccessfulResponse(result);
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

        public async Task<Exception> HandleUnsuccessfulResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    return new TrueNASAuthorisationException("The server returned a 401 code. Check your API key and try again.");
                default:
                    return new HttpRequestException($"The server returned a {response.StatusCode} status with the response body of {await response.Content.ReadAsStringAsync()}");
            }
        }

        #endregion

        #region private properties

        #endregion
    }
}
