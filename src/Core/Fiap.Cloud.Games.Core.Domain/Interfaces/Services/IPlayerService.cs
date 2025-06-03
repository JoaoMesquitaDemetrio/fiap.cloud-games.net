using Fiap.Cloud.Games.Core.Domain.Entities;

namespace Fiap.Cloud.Games.Core.Domain.Interfaces.Services;

public interface IPlayerService : IDisposable
{
    Task<Player> GetById(Guid id, bool tracking = false);
    Task Insert(Player player);
}
