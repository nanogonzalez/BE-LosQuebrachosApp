using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IDestinoDeCargaRepository
    {
        Task<DestinoDeCarga> AddDestinoDeCarga(DestinoDeCarga destinoDeCarga);
        Task<PagedResponse<IList<DestinoDeCargaDto>>> GetListDestinoDeCarga(PaginationFilter filter, string route);
        Task<DestinoDeCarga> GetDestinoDeCarga(int id);
        Task DeleteDestinoDeCarga(DestinoDeCarga destinoDeCarga);
        Task UpdateDestinoDeCarga(DestinoDeCarga destinoDeCarga);
        Task<PagedResponse<IList<DestinoDeCargaDto>>> GetDestinoDeCargaByCliente(PaginationFilter filter, string route, int idCliente);
    }
}
