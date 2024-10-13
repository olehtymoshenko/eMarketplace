using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Persistence.Extensions;
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogDatabase");

        services.AddDbContextPool<CatalogDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString, opt =>
            {
                opt.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
            });
        });

        return services;
    }

}
