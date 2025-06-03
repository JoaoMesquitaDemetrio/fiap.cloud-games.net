using Fiap.Cloud.Games.Core.Domain.Entities;

namespace Fiap.Cloud.Games.Core.Domain.Interfaces.Services;

public interface IGameService : IDisposable
{
    Task<Game> GetById(Guid id, bool tracking = false);
    Task<Game> Insert(Game game);
    Task<Game> Update(Game game);
}
