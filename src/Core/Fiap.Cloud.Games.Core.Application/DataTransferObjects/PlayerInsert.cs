

using Fiap.Cloud.Games.Core.Domain.Entities;

namespace Fiap.Cloud.Games.Core.Application.DataTransferObjects;

public record PlayerInsert(
    string Name,
    string Email,
    string Password
);

public static class PlayerInsertExtensions
{
    public static Player ToEntity(this PlayerInsert playerInsert)
    {
        return new Player(
            playerInsert.Name,
            playerInsert.Email,
            playerInsert.Password
        );
    }
}
    