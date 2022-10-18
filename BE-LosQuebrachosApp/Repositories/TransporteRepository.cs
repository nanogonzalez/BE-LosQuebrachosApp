using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Repositories
{
    public class TransporteRepository: ITransporteRepository
    {
        private readonly ApplicationDbContext _context;

        public TransporteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Transporte> AddTransporte(Transporte transporte)
        {
            _context.Transportes.Add(transporte);
            await _context.SaveChangesAsync();  
            return transporte;
        }

        public async Task DeleteTransporte(Transporte transporte)
        {
            _context.Transportes.Remove(transporte);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transporte>> GetListTransportes()
        {
            return await _context.Transportes.ToListAsync();
        }

        public async Task<Transporte> GetTransporte(int id)
        {
            return await _context.Transportes.FindAsync(id);
        }

        public async Task UpdateTransporte(Transporte transporte)
        {
            var transporteItem = await _context.Transportes.FirstOrDefaultAsync(x => x.Id == transporte.Id);

            if (transporteItem != null)
            {
                transporteItem.Nombre = transporte.Nombre;
                transporteItem.Apellido = transporte.Apellido;
                transporteItem.Cuit = transporte.Cuit;

                await _context.SaveChangesAsync();
            }
        }
    }
}
