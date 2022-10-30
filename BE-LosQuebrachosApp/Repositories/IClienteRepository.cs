using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> AddCliente(Cliente cliente);
        Task<List<Cliente>> GetListClientes();
        Task<Cliente> GetCliente(int id);
        Task DeleteCliente(Cliente cliente);

        Task UpdateCliente(Cliente cliente);
    }
}
