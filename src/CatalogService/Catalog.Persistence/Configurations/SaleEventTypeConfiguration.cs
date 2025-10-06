using Catalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.Configurations;
internal class SaleEventTypeConfiguration : IEntityTypeConfiguration<SaleEvent>
{
    public void Configure(EntityTypeBuilder<SaleEvent> builder)
    {
        builder.Property(p => p.CreatedAt)
           .HasDefaultValueSql("now() at time zone 'utc'")
           .ValueGeneratedOnAdd()
           .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

        builder.Property(p => p.Version)
            .IsRowVersion();

        builder.HasOne(v => v.Client)
            .WithMany(c => c.SaleEvents)
            .HasForeignKey(v => v.ClientId);

        builder.HasMany(p => p.Vehicles)
            .WithMany(p => p.SaleEvents)
            .UsingEntity<SaleEventVehicle>()
                .Property(p => p.Discount)
                .HasColumnType("decimal(4, 2)");

        builder.ToTable("sale_events");
    }
}
