using Fiap.Cloud.Games.Core.Domain.Entities;
using Fiap.Cloud.Games.Core.Domain.Extensions;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Cloud.Games.Core.Domain.Services;

public class GameService : BaseService, IGameService
{
    public GameService(IApplicationDBContext applicationDBContext) : base(applicationDBContext) { }

    public async Task<Game> GetById(Guid id, bool tracking = false)
    {
        var query = ById<Game>(id, tracking);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<Game> Insert(Game game)
    {
        await _context.Set<Game>().AddAsync(game);
        return game;
    }

    public async Task<Game> Update(Game game)
    {
        await _context.Set<Game>().UpdateAsync(game);
        return game;
    }
}
