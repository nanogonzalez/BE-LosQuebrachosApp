using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IOrdenDeCargaRepository
    {
        Task<OrdenDeCarga> AddOrdenDeCarga(OrdenDeCarga ordenDeCarga);
        Task<PagedResponse<List<OrdenDeCarga>>> GetListOrdenDeCarga(PaginationFilter filter, string route);
        Task<OrdenDeCarga> GetOrdenDeCarga(int id);
        Task DeleteOrdenDeCarga(OrdenDeCarga ordenDeCarga);
        Task UpdateOrdenDeCarga(OrdenDeCarga ordenDeCarga);
    }
}
