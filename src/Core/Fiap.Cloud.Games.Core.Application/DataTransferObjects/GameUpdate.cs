
namespace Fiap.Cloud.Games.Core.Application.DataTransferObjects;

public record GameUpdate(
    Guid Id,
    string Name,
    string Studio,
    decimal Price
);
