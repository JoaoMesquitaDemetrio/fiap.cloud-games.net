
using Fiap.Cloud.Games.Core.Domain.Entities;

namespace Fiap.Cloud.Games.Core.Application.DataTransferObjects;

public record GameInsert(
    string Name,
    string Studio,
    decimal Price,
    int AgeRating
);

public static class GameInsertExtensions
{
    public static Game ToEntity(this GameInsert gameInsert)
    {
        return new Game(
            gameInsert.Name,
            gameInsert.Studio,
            gameInsert.Price,
            gameInsert.AgeRating
        );
    }
}

