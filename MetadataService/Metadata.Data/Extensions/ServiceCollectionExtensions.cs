using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Metadata.Data
{
    /// <summary>
    ///
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IMetadataDbContext, MetadataDbContext>();

            ConfigureDatabase(services);
            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = "";
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
