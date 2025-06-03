using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Fiap.Cloud.Games.Core.Infra.Filters;

public class HttpInterceptionCorrelation
{
    private readonly RequestDelegate _next;

    public HttpInterceptionCorrelation(RequestDelegate next)
        => _next = next;

    public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        correlationIdGenerator.Set(Guid.NewGuid().ToString());
        context.TraceIdentifier = correlationIdGenerator.Get();

        await _next(context);
    }
}

public static class HttpInterceptionCorrelationExtensions
{
    public static IApplicationBuilder UseInterceptionCorrelation(this IApplicationBuilder app)
        => app.UseMiddleware<HttpInterceptionCorrelation>();
}
