using System.ComponentModel;

namespace Fiap.Cloud.Games.Core.Domain.Enums;

public enum TypePlayer
{
    [Description("Usuario")]
    User = 1,

    [Description("Administrador")]
    Administrator = 2
}
