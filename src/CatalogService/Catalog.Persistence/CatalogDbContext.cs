using Catalog.Persistence.Configurations;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Entities.Abstractions;
using Catalog.Persistence.Enums;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence;
public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) 
    {
        base.SavingChanges += UpdateAuditableFields;
    }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<SaleEvent> SaleEvents { get; set; }

    public DbSet<SaleEventVehicle> SaleEventVehicles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresEnum<VehicleCategory>();

        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new SaleEventTypeConfiguration());
    }


    private void UpdateAuditableFields(object? sender, SavingChangesEventArgs eventArgs)
    {
        // CreatedAt is handled by a default value database feature (see EF type configuration)
        foreach(var entry in ChangeTracker.Entries().Where(e => e.Entity is IAuditableEntity && e.State == EntityState.Modified)) 
        {
            var auditableEntity = entry.Entity as IAuditableEntity;
            if (auditableEntity == null)
            {
                // log warning
                continue;
            }
            
            auditableEntity.UpdatedAt = DateTime.UtcNow;
        }
    }

}