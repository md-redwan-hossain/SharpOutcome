using System.Reflection;

namespace SharpOutcome.HttpApiExample.Utils;

public static class ApiEndpointExtensions
{
    public static IServiceCollection MapEndpointServices(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IApiEndpoint)) && t.IsAbstract is false);

        foreach (var type in types)
        {
            var instance = ActivatorUtilities.CreateInstance(services.BuildServiceProvider(), type) as IApiEndpoint;
            instance?.RegisterServices(services);
        }

        return services;
    }

    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IApiEndpoint)) && t.IsAbstract is false);

        foreach (var type in types)
        {
            var instance = ActivatorUtilities.CreateInstance(endpoints.ServiceProvider, type) as IApiEndpoint;
            instance?.DefineRoutes(endpoints);
        }

        return endpoints;
    }
}