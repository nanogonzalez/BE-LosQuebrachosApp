using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IChoferRepository
    {
        Task<Chofer> AddChofer(Chofer chofer);
        Task DeleteChofer(Chofer chofer);
        Task<PagedResponse<IList<ChoferDto>>> GetListChoferes(PaginationFilter filter, string route);
        Task<Chofer> GetChofer(int id);
        Task UpdateChofer(Chofer chofer);
    }
}
