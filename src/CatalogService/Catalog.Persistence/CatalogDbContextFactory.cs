using Catalog.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Catalog.Persistence;
internal class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    // TODO: read config from appsettings
    const string ConnectionString = "Server=localhost;Port=5432;Database=catalog-api-database;User Id=catalog-api-app;Password=catalog-api-pass";

    public CatalogDbContext CreateDbContext(string[] args)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnectionString);
        dataSourceBuilder.MapEnum<VehicleCategory>();

        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseNpgsql(dataSourceBuilder.Build(), opt =>
        {
            opt.MigrationsAssembly(typeof(CatalogDbContext).Assembly.GetName().Name);
        });

        return new CatalogDbContext(optionsBuilder.Options);
    }
}
