using MinimalApi.Endpoint.Extensions;

namespace AsyncProductApi;

public static class ConfigureApi
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddEndpoints();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.MapEndpoints();

        return app;
    }
}