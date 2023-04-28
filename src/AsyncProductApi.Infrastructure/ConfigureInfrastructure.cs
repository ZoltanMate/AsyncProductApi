using AsyncProductApi.Core.Product.Repositories;
using AsyncProductApi.Core.RequestInfo.Repositories;
using AsyncProductApi.Infrastructure.Data;
using AsyncProductApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncProductApi.Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContextFactory<AppDbContext>(options =>
        {
            options
                .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
                .UseSqlite("Data Source=RequestDb.db");
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRequestInfoRepository, RequestInfoRepository>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder applicationBuilder)
    {
        CreateDatabase(applicationBuilder);

        return applicationBuilder;
    }

    private static void CreateDatabase(IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var scopedProvider = scope.ServiceProvider;
        
        var db = scopedProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}