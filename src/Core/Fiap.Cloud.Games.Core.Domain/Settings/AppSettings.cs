using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Cloud.Games.Core.Domain.Settings;

public record AppSettings(
    ConnectionStringsSettings ConnectionStrings
);

public static class AppSettingsExtensions
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<AppSettings>(configuration);
}