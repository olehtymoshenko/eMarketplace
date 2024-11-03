namespace Catalog.Persistence.Entities.Abstractions;
internal interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
