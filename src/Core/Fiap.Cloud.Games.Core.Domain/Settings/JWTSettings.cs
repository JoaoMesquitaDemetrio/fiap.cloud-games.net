namespace Fiap.Cloud.Games.Core.Domain.Settings;

public class JWTSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }

    public JWTSettings() { }
}
