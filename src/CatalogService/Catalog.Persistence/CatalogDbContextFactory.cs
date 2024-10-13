using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Catalog.Persistence;
internal class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    const string ConnectionString = "Server=localhost;Port=5432;Database=catalog-api-db;User Id=catalog-api-app;Password=catalog-api-pass";

    public CatalogDbContext CreateDbContext(string[] args)
    {
        Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name);
        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
        optionsBuilder.UseNpgsql(ConnectionString, opt =>
        {
            opt.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
        });

        return new CatalogDbContext(optionsBuilder.Options);
    }
}
