using Microsoft.AspNetCore.Mvc.Filters;

namespace Fiap.Cloud.Games.UI.Api.Components;

public class HttpInterceptionCorrelation : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var correlationId = Guid.NewGuid();
        context.HttpContext.TraceIdentifier = correlationId.ToString();

        await next();
    }
}
