using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Filters;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fiap.Cloud.Games.Core.Infra.Middlewares;

public class BaseLogger<T> : IBaseLogger<T> where T : class
{
    protected readonly ILogger<T> _logger;
    protected readonly ICorrelationIdGenerator _correlationId;

    public BaseLogger(ILogger<T> logger, ICorrelationIdGenerator correlationId)
    {
        _logger = logger;
        _correlationId = correlationId;
    }

    public virtual void LogInformation(string message)
        => _logger.LogInformation($"[CorrelationId: {_correlationId.Get()}] {message}");

    public virtual void LogError(string message)
        => _logger.LogError($"[CorrelationId: {_correlationId.Get()}] {message}");

    public virtual void LogWarning(string message)
        => _logger.LogWarning($"[CorrelationId: {_correlationId.Get()}] {message}");
}

public static class BaseLoggerExtensions
{
    public static void SetupBaseLogger(this IServiceCollection services)
        => services.AddScoped(typeof(IBaseLogger<>), typeof(BaseLogger<>)); 
}
