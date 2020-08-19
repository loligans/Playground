using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Storage.Api.Extensions
{
    /// <summary>
    /// The organized group of extensions for starting up the service.
    /// </summary>
    public static class OAuthServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the authentication authority and required api scopes to use the service.
        /// </summary>
        public static IServiceCollection AddStorageAuthentication(this IServiceCollection services)
        {
            // Registers the Identity Server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // Registers all valid scopes this Api accepts
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "storage");
                });
            });

            return services;
        }
    }
}
