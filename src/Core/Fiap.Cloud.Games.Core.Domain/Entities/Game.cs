
namespace Fiap.Cloud.Games.Core.Domain.Entities;

public class Game : Identifier
{
    public string Name { get; set; }
    public string Studio { get; set; }
    public decimal Price { get; set; }
    public int AgeRating { get; set; }

    public Game() : base() { }

    public Game(string name, string studio, decimal price, int ageRating) : this()
    {
        Name = name;
        Studio = studio;
        Price = price;
        AgeRating = ageRating;
    }
}
