namespace BuildingBlocks.Core.SeedWork;

public class PagedList<T> : List<T>{

    private MetaData _meta;

    public PagedList(IEnumerable<T> items, long totalItems, int pageNumber, int pageSize){
        _meta = new MetaData(){
            TotalItems = totalItems,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotolPages = (int)Math.Ceiling(totalItems/(double)pageSize),
        };
        AddRange(items);
    }

    public MetaData GetMetaData(){
        return _meta;
    }
}