
using Fiap.Cloud.Games.Core.Domain.Entities;

namespace Fiap.Cloud.Games.Core.Application.DataTransferObjects;

public record GameUpdate(
    Guid Id,
    string Name,
    string Studio,
    decimal Price
)
{
    public Game UpdateEntity(Game entity)
    {
        entity.Name = Name;
        entity.Studio = Studio;
        entity.Price = Price;

        return entity;
    }
};

public static class GameUpdateExtensions
{
    public static Game UpdateEntity(this GameUpdate gameUpdate, Game game)
    {
        game.Name = gameUpdate.Name;
        game.Studio = gameUpdate.Studio;
        game.Price = gameUpdate.Price;

        return game;
    }

    public static GameUpdate Parse(this Game game)
    {
        return new GameUpdate(
            game.Id,
            game.Name,
            game.Studio,
            game.Price
        );
    }
}
