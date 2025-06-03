

namespace Fiap.Cloud.Games.Core.Application.DataTransferObjects;

public record PlayerInsert(
    string Name,
    string Email,
    string PasswordHash
);
    