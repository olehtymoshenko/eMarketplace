using Catalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.Configurations;
internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        // TODO: Override defaults

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("now() at time zone 'utc'");

        builder.Property(p => p.Propeprties)
            .HasColumnType("jsonb");

        builder.Property(p => p.Price)
            .HasColumnType("decimal(10,2)");

        builder.HasOne(v => v.Client)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.VehicleId);
    }
}
