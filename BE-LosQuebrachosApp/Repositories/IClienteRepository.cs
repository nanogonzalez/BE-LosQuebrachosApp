using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> AddCliente(Cliente cliente);
        Task<PagedResponse<List<Cliente>>> GetListCliente(PaginationFilter filter, string route);
        Task<Cliente> GetCliente(int id);
        Task DeleteCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);

    }
}
