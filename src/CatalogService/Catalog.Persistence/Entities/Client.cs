using Catalog.Persistence.Entities.Abstractions;

namespace Catalog.Persistence.Entities;

public class Client : BaseEntity, IAuditableEntity
{
    public string? Name { get; set; }

    public string? LocationRegion { get; set; }

    public string? City { get; set; }

    public string? ZipCode { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public uint Version { get; set; }


    public IEnumerable<Vehicle>? Vehicles { get; set; }

    public IEnumerable<SaleEvent>? SaleEvents { get; set; }
}
