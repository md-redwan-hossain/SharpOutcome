namespace SharpOutcome.HttpApiExample.Utils;

public interface IApiEndpoint
{
    void RegisterServices(IServiceCollection services);
    void DefineRoutes(IEndpointRouteBuilder routes);
}
