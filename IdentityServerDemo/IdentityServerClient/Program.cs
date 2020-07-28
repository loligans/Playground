using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityServerClient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            // Discover endpoints from metadata
            var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            if(discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return;
            }

            // Request a Token
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                ClientId = "demoId",
                ClientSecret = "DemoConfidential",

                Scope = "demoScope"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            // Debug: Print out Api token
            Console.WriteLine($"{tokenResponse.Json}{Environment.NewLine}");

            // Call Api using token
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // var response = await apiClient.GetAsync("");
            // if (!response.IsSuccessStatusCode)
            // {
            //     Console.WriteLine($"{response.StatusCode}: {response?.ReasonPhrase}");
            //     return;
            // }

            // var content = await response.Content.ReadAsStringAsync();
            // Console.WriteLine(content);
        }
    }
}
