
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Fiap.Cloud.Games.Core.Application.Interfaces;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Services;
using Sample.Utils.Extensions;

namespace Fiap.Cloud.Games.Core.Application;

public class GameApplicationService : BaseApplicationService, IGameApplicationService
{
    private readonly IGameService _gameService;

    public GameApplicationService(IApplicationDBContext applicationDBContext, IGameService gameService) : base(applicationDBContext)
        => _gameService = gameService;

    public async Task<GameUpdate> Insert(GameInsert game)
    {
        var entity = game.ToEntity();

        await _gameService.Insert(entity);
        await SaveChangesAsync();

        return entity.Parse();
    }

    public async Task<GameUpdate> Update(GameUpdate game)
    {
        var entity = await _gameService.GetById(game.Id, true);
        if (entity.IsNull())
            throw new ArgumentException($"Jogo com ID {game.Id} não encontrado.");

        game.UpdateEntity(entity);

        await _gameService.Update(entity);
        await SaveChangesAsync();

        return entity.Parse();
    }

    public override void Dispose()
    {
        _gameService?.Dispose();

        GC.SuppressFinalize(this);
    }
}
