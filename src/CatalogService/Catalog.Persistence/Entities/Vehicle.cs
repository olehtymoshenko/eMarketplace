using Catalog.Persistence.Enums;

namespace Catalog.Persistence.Entities;
public class Vehicle : BaseEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Propeprties { get; set; }

    public int Quantity { get; set; }

    public VehicleCategory Category { get; set; }
}
