using System.Reflection;

namespace SharpOutcome.HttpApiExample.Utils;

public static class ApiEndpointExtensions
{
    public static IServiceCollection MapApiEndpointServices(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes().Where(t =>
            t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(typeof(IApiEndpoint)));


        foreach (var type in types)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length > 1 || (constructors.Length == 1 && constructors[0].GetParameters().Length > 0))
            {
                throw new InvalidOperationException($"Type {type.FullName} must only have a empty constructor.");
            }

            var instance = Activator.CreateInstance(type) as IApiEndpoint;
            instance?.RegisterServices(services);
        }

        return services;
    }

    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints, Assembly assembly)
    {
        var types = assembly.GetTypes().Where(t =>
            t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(typeof(IApiEndpoint)));

        foreach (var type in types)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length > 1 || (constructors.Length == 1 && constructors[0].GetParameters().Length > 0))
            {
                throw new InvalidOperationException($"Type {type.FullName} must only have a empty constructor.");
            }

            var instance = Activator.CreateInstance(type) as IApiEndpoint;
            instance?.DefineRoutes(endpoints);
        }

        return endpoints;
    }
}