namespace Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Filters;

public interface ICorrelationIdGenerator
{
    string Get();
    void Set(string correlationId);
}
