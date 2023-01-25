using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IOrdenDeGasoilRepository
    {
        Task<OrdenDeGasoil> AddOrdenDeGasoil(OrdenDeGasoil ordenDeGasoil);
        Task DeleteOrdenDeGasoil(OrdenDeGasoil ordenDeGasoil);
        Task<PagedResponse<IList<OrdenDeGasoilDto>>> GetListOrdenDeGasoil(PaginationFilter filter, string route);
        Task<OrdenDeGasoil> GetOrdenDeGasoil(int id);
        Task UpdateOrdenDeGasoil(OrdenDeGasoil ordenDeGasoil);

    }
}
