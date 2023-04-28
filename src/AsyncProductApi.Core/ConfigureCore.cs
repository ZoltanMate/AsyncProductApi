using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncProductApi.Core;

public static class ConfigureCore
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureCore).Assembly;

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        services.AddMassTransit(options =>
        {
            options.AddConsumers(assembly);
            
            options.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}