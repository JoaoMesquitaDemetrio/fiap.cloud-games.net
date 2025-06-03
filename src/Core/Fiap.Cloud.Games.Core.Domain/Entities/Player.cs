
using Fiap.Cloud.Games.Core.Domain.Enums;

namespace Fiap.Cloud.Games.Core.Domain.Entities;

public class Player : Identifier
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public TypePlayer Type { get; set; }

    public Player() : base() { }

    public Player(string name, string email, string password) : this()
    {
        Name = name;
        Email = email;
        PasswordHash = password;
    }

    public void SetType(TypePlayer type)
        => Type = type;
}
