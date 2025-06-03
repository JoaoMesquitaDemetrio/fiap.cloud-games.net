
namespace Fiap.Cloud.Games.Core.Domain.Entities;

public class Game : Identifier
{
    public string Name { get; set; }
    public string Studio { get; set; }
    public decimal Price { get; set; }
    public int AgeRating { get; set; }

    public Game() : base() { }
}
