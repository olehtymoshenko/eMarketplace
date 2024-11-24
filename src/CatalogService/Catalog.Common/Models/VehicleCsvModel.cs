using Catalog.Common.Enums;

namespace Catalog.Business.Models;
public class VehicleCsvModel
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Properties { get; set; }

    public int Quantity { get; set; }

    public VehicleCategory Category { get; set; }
}
