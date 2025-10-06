namespace Catalog.Common.Models;

public class PaginationQuery
{
    public PaginationQuery() : this(50, 1) { }

    public PaginationQuery(int pageSize, int pageNumber)
    {
        PageSize = pageSize <= 0 ? 50 : pageSize;
        PageNumber = pageNumber <= 0 ? 1 : pageNumber;
    }

    public int PageSize { get; init; }

    public int PageNumber { get; init; }
}
