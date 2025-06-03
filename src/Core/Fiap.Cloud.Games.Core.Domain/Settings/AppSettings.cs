using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Cloud.Games.Core.Domain.Settings;

public class AppSettings
{
    public ConnectionStringsSettings ConnectionStrings { get; set; } 
    public JWTSettings Jwt { get; set; }

    public AppSettings() { }
}

public static class AppSettingsExtensions
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<AppSettings>(configuration);
}