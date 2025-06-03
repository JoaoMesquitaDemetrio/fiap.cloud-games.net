
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Fiap.Cloud.Games.Core.Application.Interfaces;
using Fiap.Cloud.Games.Core.Domain.Enums;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Services;

namespace Fiap.Cloud.Games.Core.Application;

public class PlayerApplicationService : BaseApplicationService, IPlayerApplicationService
{
    private readonly IPlayerService _playerService;

    public PlayerApplicationService(IApplicationDBContext applicationDBContext, IPlayerService playerService) : base(applicationDBContext)
        => _playerService = playerService;

    public async Task InsertAsUser(PlayerInsert player)
    {
        var entity = player.ToEntity();
        entity.SetType(TypePlayer.User);

        await _playerService.Insert(entity);
        await SaveChangesAsync();
    }

    public async Task InsertAsAdministrator(PlayerInsert player)
    {
        var entity = player.ToEntity();
        entity.SetType(TypePlayer.Administrator);

        await _playerService.Insert(entity);
        await SaveChangesAsync();
    }

    public override void Dispose()
    {
        _playerService?.Dispose();

        GC.SuppressFinalize(this);
    }
}
