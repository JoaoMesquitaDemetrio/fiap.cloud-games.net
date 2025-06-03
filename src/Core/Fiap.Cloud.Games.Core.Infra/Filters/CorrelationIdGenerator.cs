using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Filters;

namespace Fiap.Cloud.Games.Core.Infra.Filters;

public class CorrelationIdGenerator : ICorrelationIdGenerator
{
    private string _correlationId;

    public string Get() => _correlationId;

    public void Set(string correlationId) => _correlationId = correlationId;
}