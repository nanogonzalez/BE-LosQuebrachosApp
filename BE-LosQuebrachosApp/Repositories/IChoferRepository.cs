using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IChoferRepository
    {
        Task<Chofer> AddChofer(Chofer chofer);
        Task DeleteChofer(Chofer chofer);
        Task<List<Chofer>> GetListChoferes();
        Task<Chofer> GetChofer(int id);
        Task UpdateChofer(Chofer chofer);
    }
}
