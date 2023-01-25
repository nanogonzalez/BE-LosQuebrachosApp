using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IDestinoDeDescargaRepository
    {
        Task<DestinoDeDescarga> AddDestinoDeDescarga(DestinoDeDescarga destinoDeDescarga);
        Task DeleteDestinoDeDescarga(DestinoDeDescarga destinoDeDescarga);
        Task<PagedResponse<IList<DestinoDeDescargaDto>>> GetListDestinoDeDescarga(PaginationFilter filter, string route);
        Task<DestinoDeDescarga> GetDestinoDeDescarga(int id);
        Task UpdateDestinoDeDescarga(DestinoDeDescarga destinoDeDescarga);
    }
}
