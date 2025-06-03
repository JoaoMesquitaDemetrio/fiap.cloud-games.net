using Fiap.Cloud.Games.Core.Application.DataTransferObjects;

namespace Fiap.Cloud.Games.Core.Application.Interfaces;

public interface IGameApplicationService : IDisposable
{
    Task<GameUpdate> Insert(GameInsert game);
    Task<GameUpdate> Update(GameUpdate game);
}
