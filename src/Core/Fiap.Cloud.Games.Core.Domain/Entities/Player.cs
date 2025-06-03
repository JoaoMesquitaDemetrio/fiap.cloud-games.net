
namespace Fiap.Cloud.Games.Core.Domain.Entities;

public class Player : Identifier
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public Player() : base() { }
}
