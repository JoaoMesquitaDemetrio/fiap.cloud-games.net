namespace Fiap.Cloud.Games.Core.Domain.Entities;

public class Identifier
{
    public Guid Id { get; set; }

    public Identifier()
        => Id = Guid.NewGuid();
}
