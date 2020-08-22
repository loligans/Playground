using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;

namespace Storage.Api.Extensions
{
    public static class TusApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTus(this IApplicationBuilder app)
        {
            var authService = app.ApplicationServices.GetService<IAuthorizationService>();
            if (authService is null) {
                throw new InvalidOperationException($"{nameof(authService)} cannot be null when validating a user's authorization.");
            }

            app.UseTus(context => new DefaultTusConfiguration
            {
                UrlPath = "/files",
                Store = new TusDiskStore(@"C:\Users\loligans\Downloads\TusDatastore", true),
                Events = new Events
                {
                    OnAuthorizeAsync = async eventContext => await OnAuthorizeAsync(eventContext, authService),
                    OnFileCompleteAsync = async eventContext =>
                    {
                        var file = await eventContext.GetFileAsync();
                    }
                }
            });
            return app;
        }

        private static async Task OnAuthorizeAsync(AuthorizeContext eventContext, IAuthorizationService authService)
        {
            var authenticateResult = await eventContext.HttpContext.AuthenticateAsync();
            if (authenticateResult is null || authenticateResult.Succeeded is false)
            {
                eventContext.FailRequest(HttpStatusCode.Forbidden);
                return;
            }

            var authorizeResult = await authService.AuthorizeAsync(eventContext.HttpContext.User, "ApiScope");
            if (authorizeResult is null || authorizeResult.Succeeded is false)
            {
                eventContext.FailRequest(HttpStatusCode.Unauthorized);
                return;
            }
        }
    }
}
