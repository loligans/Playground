using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace MetadataClient
{
    class Program
    {
        public static async Task Main()
        {
            using var httpClient = new HttpClient();

            // Discover endpoints from metadata
            var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001").ConfigureAwait(false);
            if(discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return;
            }

            // Request a Token
            using var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "MetadataClient",
                ClientSecret = "DemoConfidential",

                Scope = "metadata"
            };
            var tokenResponse = await httpClient
                .RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest)
                .ConfigureAwait(false);
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            // Debug: Print out Api token
            Console.WriteLine($"{tokenResponse.Json}{Environment.NewLine}");

            // Call Api using token
            using var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // Create request object
            var requestObject = new {
                Type = $"type-{Guid.NewGuid()}",
                Path = $"path-{Guid.NewGuid()}",
                Description = $"description-{Guid.NewGuid()}",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Size = 1024
            };
            var requestBody = System.Text.Json.JsonSerializer.Serialize(requestObject);
            using var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Create post request
            var endpoint = new Uri("https://localhost:6001/metadata");
            var response = await apiClient.PostAsync(endpoint, requestContent).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{response.StatusCode}: {response?.ReasonPhrase}");
                return;
            }

            // Create get request
            endpoint = new Uri(endpoint, "metadata/e1109609-d887-474e-8519-08d838487987");
            response = await apiClient.GetAsync(endpoint).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Console.WriteLine(content);
        }
    }
}
