using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IOrdenDeCargaRepository
    {
        Task<OrdenDeCarga> AddOrdenDeCarga(OrdenDeCarga ordenDeCarga);
        Task<List<OrdenDeCarga>> GetListOrdenDeCarga();
        Task<OrdenDeCarga> GetOrdenDeCarga(int id);
        Task DeleteOrdenDeCarga(OrdenDeCarga ordenDeCarga);
        Task UpdateOrdenDeCarga(OrdenDeCarga ordenDeCarga);
    }
}
