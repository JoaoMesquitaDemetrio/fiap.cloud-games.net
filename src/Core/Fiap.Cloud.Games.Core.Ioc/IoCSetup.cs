using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sample.Utils.Extensions;
using APP = Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using DOM = Fiap.Cloud.Games.Core.Domain.Entities;

namespace Fiap.Cloud.Games.Core.Ioc;

public static class IoCSetup
{
    public static void SetupIoC(this IServiceCollection services)
    {
        services.ApplyToFrameworkServices();
    }

    internal static void ApplyToFrameworkServices(this IServiceCollection services)
    {
        static void HandlerInterface(IServiceCollection services, IEnumerable<TypeInfo> assemblyInterfaces, IEnumerable<TypeInfo> assemblyServices)
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

        var appServices = Assembly.GetAssembly(typeof(APP.HttpResponse.ResponseError))?.DefinedTypes;
        var appInterfaces = appServices?.Where(t => t.IsInterface)?.ToList();
        HandlerInterface(services, appInterfaces.GetValueOrDefault(), appServices.GetValueOrDefault());

        var domainTypes = Assembly.GetAssembly(typeof(DOM.Identifier))?.DefinedTypes;
        var serviceInterfaces = domainTypes?.Where(t => t.IsInterface)?.ToList();
        HandlerInterface(services, serviceInterfaces.GetValueOrDefault(), domainTypes.GetValueOrDefault());
    }
}
