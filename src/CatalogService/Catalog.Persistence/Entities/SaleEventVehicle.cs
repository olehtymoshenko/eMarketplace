namespace Catalog.Persistence.Entities;
public class SaleEventVehicle
{
    /// <summary>
    /// In percents
    /// </summary>
    public decimal Discount { get; set; }
    
    public int SaleEventId { get; set; }

    public int VehicleId { get; set; }


    public SaleEvent? SaleEvent { get; set; }

    public Vehicle? Vehicle { get; set; }

}
