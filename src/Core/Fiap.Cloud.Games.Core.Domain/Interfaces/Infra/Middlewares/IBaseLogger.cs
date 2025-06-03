namespace Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Middlewares;

public interface IBaseLogger<T> where T : class
{
    void LogInformation(string message);
    void LogError(string message);
    void LogWarning(string message);   
}
