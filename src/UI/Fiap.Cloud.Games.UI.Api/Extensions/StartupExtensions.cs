using Fiap.Cloud.Games.UI.Api.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Fiap.Cloud.Games.UI.Api.Extensions;

public static class StartupExtensions
{
    internal static void SetupGlobalRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        => opts.Conventions.Insert(0, new RouteConvention(routeAttribute));
}
