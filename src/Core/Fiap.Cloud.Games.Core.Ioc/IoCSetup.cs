using System.Reflection;
using Fiap.Cloud.Games.Core.Infra.Middlewares;
using Fiap.Cloud.Games.Core.Infra.Repositories.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.Utils.Extensions;
using APP = Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using DOM = Fiap.Cloud.Games.Core.Domain.Entities;
using IFR = Fiap.Cloud.Games.Core.Infra.Filters; 

namespace Fiap.Cloud.Games.Core.Ioc;

public static class IoCSetup
{
    public static void SetupIoC(this IServiceCollection services)
    {
        services.ApplyToFrameworkServices();
        services.ApplyToInfraServices();
    }

    internal static void ApplyToInfraServices(this IServiceCollection services)
    {
        var domainTypes = Assembly.GetAssembly(typeof(DOM.Identifier))?.DefinedTypes;
        var domainInterfaces = domainTypes?.Where(t => t.IsInterface)?.ToList();
        var infraServices = Assembly.GetAssembly(typeof(IFR.CorrelationIdGenerator))?.DefinedTypes;
        HandlerInterface(services, domainInterfaces.GetValueOrDefault(), infraServices.GetValueOrDefault());

        services.SetupHandlerException();
        services.SetupBaseLogger();
        services.SetupApplicationDBContext();
        services.AddLogging(opt => opt.AddSimpleConsole(c => c.TimestampFormat = "[HH:mm:ss] "));
    }

    internal static void ApplyToFrameworkServices(this IServiceCollection services)
    {
        var appServices = Assembly.GetAssembly(typeof(APP.HttpResponse.ExceptionResult))?.DefinedTypes;
        var appInterfaces = appServices?.Where(t => t.IsInterface)?.ToList();
        HandlerInterface(services, appInterfaces.GetValueOrDefault(), appServices.GetValueOrDefault());

        var domainTypes = Assembly.GetAssembly(typeof(DOM.Identifier))?.DefinedTypes;
        var serviceInterfaces = domainTypes?.Where(t => t.IsInterface)?.ToList();
        HandlerInterface(services, serviceInterfaces.GetValueOrDefault(), domainTypes.GetValueOrDefault());
    }

    internal static void HandlerInterface(IServiceCollection services, IEnumerable<TypeInfo> assemblyInterfaces, IEnumerable<TypeInfo> assemblyServices)
    {
        foreach (var appInterface in assemblyInterfaces.GetValueOrDefault())
        {
            var appService = assemblyServices?
                .Where(s => !s.IsAbstract && !s.IsSealed && s.IsClass)
                .FirstOrDefault(s => s.ImplementedInterfaces.Contains(appInterface));

            if (appService == null)
                continue;

            services.AddScoped(appInterface, appService.AsType());
        }
    }
}
