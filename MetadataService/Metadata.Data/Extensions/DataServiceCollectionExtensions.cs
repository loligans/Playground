using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metadata.Data.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMetadataDbContext, MetadataDbContext>();

            var connectionString = configuration.GetValue<string>("MetadataConnectionString");
            ConfigureDatabase(services, connectionString);
            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<MetadataDbContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                    }
                )
            );
        }
    }
}
