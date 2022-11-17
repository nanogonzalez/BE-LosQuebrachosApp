using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface ITransporteRepository
    {
        Task<Transporte> AddTransporte(Transporte transporte);
        Task DeleteTransporte(Transporte transporte);
        Task<PagedResponse<List<Transporte>>> GetListTransportes(PaginationFilter filter, string route);
        Task<Transporte> GetTransporte(int id);
        Task UpdateTransporte(Transporte transporte);
    }
}
