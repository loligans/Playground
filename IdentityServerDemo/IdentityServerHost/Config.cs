using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServerHost
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("demoScope", "Demo Scope")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "demoId",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("DemoConfidential".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "demoScope" }
                }
            };
    }
}