using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Stores;

namespace Storage.Api.Extensions
{
    public static class TusApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTus(this IApplicationBuilder app)
        {
            app.UseTus(context => new DefaultTusConfiguration
            {
                UrlPath = "/files",
                Store = new TusDiskStore(@"C:\Users\loligans\Downloads\TusDatastore", true),
                Events = new tusdotnet.Models.Configuration.Events
                {
                    OnAuthorizeAsync = async eventContext =>
                    {
                        var authService = app.ApplicationServices.GetService<IAuthorizationService>();
                        if (authService is null) throw new InvalidOperationException($"{nameof(authService)} cannot be null when validating a user's authorization.");

                        var authenticateResult = await eventContext.HttpContext.AuthenticateAsync();
                        var authorizeResult = await authService.AuthorizeAsync(eventContext.HttpContext.User, "ApiScope");

                        var status = (authenticateResult, authorizeResult) switch
                        {
                            var (authenticate, authorize) when authenticate.Succeeded && authorize.Succeeded => HttpStatusCode.OK,
                            var (authenticate, authorize) when !authenticate.Succeeded && authorize.Succeeded => HttpStatusCode.Forbidden,
                            var (authenticate, authorize) when authenticate.Succeeded && !authorize.Succeeded => HttpStatusCode.Unauthorized,
                            var (_, _) => HttpStatusCode.Forbidden
                        };
                        
                        if (status is HttpStatusCode.Forbidden || status is HttpStatusCode.Unauthorized)
                        {
                            eventContext.FailRequest(status);
                        }
                    },
                    OnFileCompleteAsync = async eventContext =>
                    {
                        var file = await eventContext.GetFileAsync();
                    }
                }
            });
            return app;
        }

    }
}
