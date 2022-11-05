using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Repositories
{
    public class OrdenDeCargaRepository: IOrdenDeCargaRepository
    {
        private readonly ApplicationDbContext _context;
        public OrdenDeCargaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrdenDeCarga> AddOrdenDeCarga(OrdenDeCarga ordenDeCarga)
        {
            _context.OrdenesDeCargas.Add(ordenDeCarga);
            await _context.SaveChangesAsync();
            return ordenDeCarga;
        }

        public async Task<List<OrdenDeCarga>> GetListOrdenDeCarga()
        {
            return await _context.OrdenesDeCargas.ToListAsync();
        }

        public async Task<OrdenDeCarga> GetOrdenDeCarga(int id)
        {
            return await _context.OrdenesDeCargas.FindAsync(id);
        }

        public async Task DeleteOrdenDeCarga(OrdenDeCarga ordenDeCarga)
        {
            _context.OrdenesDeCargas.Remove(ordenDeCarga);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrdenDeCarga(OrdenDeCarga ordenDeCarga)
        {
            var ordenDeCargaItem = await _context.OrdenesDeCargas.FirstOrDefaultAsync(x => x.Id == ordenDeCarga.Id);

            if (ordenDeCargaItem != null)
            {
                ordenDeCargaItem.DestinoCarga = ordenDeCarga.DestinoCarga;
                ordenDeCargaItem.DestinoDescarga = ordenDeCarga.DestinoDescarga;
                ordenDeCargaItem.DiaHoraCarga = ordenDeCarga.DiaHoraCarga; 
                ordenDeCargaItem.TipoMercaderia = ordenDeCarga.TipoMercaderia;

                await _context.SaveChangesAsync();
            }
        }
    }
}

