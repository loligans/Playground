using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Api.Extensions;
using tusdotnet;

namespace Storage.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddStorageAuthentication();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseTus();
        }
    }
}
