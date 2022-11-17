using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Helpers;
using BE_LosQuebrachosApp.Services;
using BE_LosQuebrachosApp.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Repositories
{
    public class VehiculoRepository: IVehiculoRepsitory
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService uriService;

        public VehiculoRepository(ApplicationDbContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
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
        public async Task<PagedResponse<List<Vehiculo>>> GetListVehiculos(PaginationFilter filter, string route)
        {
            
            var pagedData = await _context.Vehiculos
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Vehiculos.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, totalRecords, uriService, route);
            return pagedReponse;
        }
        public async Task<Vehiculo> GetVehiculo(int id)
        {
            return await _context.Vehiculos.Where(a => a.Id == id).FirstOrDefaultAsync();
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
