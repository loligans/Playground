using System;
using Microsoft.EntityFrameworkCore;
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
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IMetadataDbContext, MetadataDbContext>();

            ConfigureDatabase(services);
            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = "MetadataConnectionString";
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
