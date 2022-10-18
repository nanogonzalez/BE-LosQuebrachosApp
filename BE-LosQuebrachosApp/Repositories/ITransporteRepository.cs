using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface ITransporteRepository
    {
        Task<Transporte> AddTransporte(Transporte transporte);
        Task DeleteTransporte(Transporte transporte);
        Task<List<Transporte>> GetListTransportes();
        Task<Transporte> GetTransporte(int id);
        Task UpdateTransporte(Transporte transporte);
    }
}
