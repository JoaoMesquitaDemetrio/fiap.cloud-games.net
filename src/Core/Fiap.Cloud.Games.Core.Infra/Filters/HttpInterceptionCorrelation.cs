using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fiap.Cloud.Games.Core.Infra.Filters;

public class HttpInterceptionCorrelation : IAsyncActionFilter
{
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public HttpInterceptionCorrelation(ICorrelationIdGenerator correlationIdGenerator)
        => _correlationIdGenerator = correlationIdGenerator;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _correlationIdGenerator.Set(Guid.NewGuid().ToString());
        context.HttpContext.TraceIdentifier = _correlationIdGenerator.Get();

        await next();
    }
}

public static class HttpInterceptionCorrelationExtensions
{
    public static void AddHttpInterceptionCorrelation(this FilterCollection filters)
        => filters.Add(typeof(HttpInterceptionCorrelation));
}
