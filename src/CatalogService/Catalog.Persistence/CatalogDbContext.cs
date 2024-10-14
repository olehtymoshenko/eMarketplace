using Catalog.Persistence.Configurations;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Enums;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence;
public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresEnum<VehicleCategory>();

        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
    }

}