namespace Catalog.Persistence.Entities;

public class Client : BaseEntity
{
    public string? Name { get; set; }

    public string? LocationRegion { get; set; }

    public string? City { get; set; }

    public string? ZipCode { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }




}
