using System.Reflection;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;
using Fiap.Cloud.Games.Core.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fiap.Cloud.Games.Core.Infra.Repositories.EF;

public class ApplicationDBContext : DbContext, IApplicationDBContext
{
    private readonly AppSettings _settings;
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationDBContext(IOptionsSnapshot<AppSettings> options, ILoggerFactory loggerFactory)
    {
        _settings = options.Value;
        _loggerFactory = loggerFactory;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.Namespace == "Fiap.Cloud.Games.Core.Infra.Repositories.EF.Mappings"
        );

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
        optionsBuilder.UseLazyLoadingProxies(false);
        optionsBuilder.UseSqlServer(_settings.ConnectionStrings.DatabaseConnection, x =>
        {
            x.MigrationsAssembly("Fiap.Cloud.Games.Core.Infra");
            x.EnableRetryOnFailure();
        });
        optionsBuilder.UseLoggerFactory(_loggerFactory);

        /*
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(opt => opt.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        */
    }
}

public static class ApplicationDBContextExtensions
{
    public static void SetupSQLDatabase(this IServiceCollection services)
    {
        services.AddEntityFrameworkSqlServer()
            .AddDbContext<ApplicationDBContext>()
            .BuildServiceProvider();
    }

    public static void SetupApplicationDBContext(this IServiceCollection services)
        => services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
}
