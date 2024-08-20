namespace BuildingBlocks.Core.SeedWork;

public class MetaData{
    public int CurrentPage { get; set; }
    public int TotolPages { get; set; }
    public int PageSize { get; set; }
    public long TotalItems { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotolPages;
    public int FirstRowOnPage => TotalItems > 0 ? (CurrentPage - 1) * PageSize + 1 : 0;
    public int LastRowOnPage => (int)Math.Min(TotalItems, CurrentPage * PageSize);
}