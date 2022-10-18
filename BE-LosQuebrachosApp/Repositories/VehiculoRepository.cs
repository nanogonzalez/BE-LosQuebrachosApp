using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Repositories
{
    public class VehiculoRepository: IVehiculoRepsitory
    {
        private readonly ApplicationDbContext _context;

        public VehiculoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
        }
        public async Task<Vehiculo> AddVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
            return vehiculo;
        }
        public async Task<List<Vehiculo>> GetListVehiculos()
        {
            return await _context.Vehiculos.ToListAsync();
        }
        public async Task<Vehiculo> GetVehiculo(int id)
        {
            return await _context.Vehiculos.FindAsync(id);
        }
        public async Task UpdateVehiculo(Vehiculo vehiculo )
        {
            var vehiculoItem = await _context.Vehiculos.FirstOrDefaultAsync(x => x.Id == vehiculo.Id);

            if (vehiculoItem != null)
            {
                vehiculoItem.Acoplado = vehiculo.Acoplado;
                vehiculoItem.Chasis = vehiculo.Chasis;
                vehiculoItem.Tipo = vehiculo.Tipo;
                vehiculoItem.CapacidadTN = vehiculo.CapacidadTN;

                await _context.SaveChangesAsync();
            }
        }
    }
}
