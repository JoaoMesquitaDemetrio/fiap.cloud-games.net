using System.Net;
using Fiap.Cloud.Games.UI.Api.Components;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;

namespace Fiap.Cloud.Games.UI.Api.Extensions;

public static class StartupExtensions
{
    internal static void SetupGlobalRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        => opts.Conventions.Insert(0, new RouteConvention(routeAttribute));

    internal static void SetupExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var handler = new HandlerException(context.TraceIdentifier);
                    var result = handler.ResponseException(contextFeature.Error);

                    context.Response.StatusCode = (int)result.StatusCode;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(result.Response));
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync("Internal Server Error");
                }
            });
        });
    }
}
