    using Fiap.Cloud.Games.Core.Application.DataTransferObjects;

namespace Fiap.Cloud.Games.Core.Application.Interfaces;

public interface IPlayerApplicationService : IDisposable
{
    Task InsertAsUser(PlayerInsert player);
    Task InsertAsAdministrator(PlayerInsert player);
}
