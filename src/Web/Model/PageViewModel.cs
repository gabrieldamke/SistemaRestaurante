namespace Web.Model;

public class PageViewModel
{
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public int TotalPages { get; set; }
    public int PageIndex { get; set; }
    
    public PageViewModel(int pageIndex, int totalPages, bool hasPreviousPage, bool hasNextPage)
    {
        PageIndex = pageIndex;
        TotalPages = totalPages;
        HasPreviousPage = hasPreviousPage;
        HasNextPage = hasNextPage;
    }
}