using Catalog.Common.Enums;
using Catalog.Persistence.Repositories;
using Catalog.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Catalog.Persistence.Extensions;
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogDatabase");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<VehicleCategory>();
        var a = typeof(CatalogDbContext).Assembly.GetName().Name;
        services.AddDbContextPool<CatalogDbContext>(opt =>
        {
            opt.UseSnakeCaseNamingConvention();
            opt.UseNpgsql(dataSourceBuilder.Build(), opt =>
            {
                //opt.MigrationsAssembly(typeof(CatalogDbContext).Assembly.GetName().Name);
                opt.MigrationsAssembly("Catalog.Persistence");
            });
        });

        services.TryAddScoped<IClientRepository, ClientRepository>();
        services.TryAddScoped<IVehicleRepository, VehicleRepository>();
        services.TryAddScoped<ISaleEventRepository, SaleEventRepository>();

        return services;
    }

}
