using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Repositories
{
    public class ChoferRepository: IChoferRepository
    {
        private readonly ApplicationDbContext _context;
        public ChoferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Chofer> AddChofer(Chofer chofer)
        {
            _context.Choferes.Add(chofer);
            await _context.SaveChangesAsync();
            return chofer;
        }

        public async Task DeleteChofer(Chofer chofer)
        {
            _context.Choferes.Remove(chofer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Chofer>> GetListChoferes()
        {
            return await _context.Choferes.ToListAsync();
        }

        public async Task<Chofer> GetChofer(int id)
        {
            return await _context.Choferes.FindAsync(id);
        }

        public async Task UpdateChofer(Chofer chofer)
        {
            var choferItem = await _context.Choferes.FirstOrDefaultAsync(x => x.Id == chofer.Id);

            if (choferItem != null)
            {
                choferItem.Nombre = chofer.Nombre;
                choferItem.Apellido = chofer.Apellido;
                choferItem.Cuit = chofer.Cuit;
                
                await _context.SaveChangesAsync();
            }
        }
    }

}
