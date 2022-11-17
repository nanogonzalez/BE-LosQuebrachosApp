using BE_LosQuebrachosApp.Filter;

namespace BE_LosQuebrachosApp.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
