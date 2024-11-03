using Catalog.Persistence.Entities.Abstractions;

namespace Catalog.Persistence.Entities;
public class SaleEvent : BaseEntity, IAuditableEntity
{
    public required string Name { get; set; }

    public DateTime DateEventStart { get; set; }

    public DateTime DateEventEnd { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint Version { get; set; }

    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public List<SaleEventVehicle> SaleEventVehicle { get; set; } = [];
    public List<Vehicle> Vehicles { get; set; } = [];
}
