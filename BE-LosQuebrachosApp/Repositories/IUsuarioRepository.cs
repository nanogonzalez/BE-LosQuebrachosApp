using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AddUsuario(Usuario usuario);
    }
}
