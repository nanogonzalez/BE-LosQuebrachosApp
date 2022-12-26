namespace BE_LosQuebrachosApp.Filter
{
    public class PaginationFilter
    {
        public string Search { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
        public PaginationFilter()
        {
            this.Search = null;
            this.SortOrder = "asc";
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize, string search, string sortOrder)
        {
            this.Search = search;
            this.SortOrder = sortOrder;
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
