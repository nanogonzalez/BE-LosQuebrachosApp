using AutoMapper;
using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Dtos;
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
        private readonly IUriService _uriService;
        private readonly IMapper mapper;

        public VehiculoRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }
        public async Task DeleteVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
        }
        public async Task<Vehiculo> AddVehiculo(Vehiculo vehiculo)
        {
            vehiculo.Transporte = await _context.Transportes.FirstOrDefaultAsync(x => x.Id == vehiculo.Transporte.Id);
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
            return vehiculo;
        }
        public async Task<PagedResponse<IList<VehiculoDto>>> GetListVehiculos(PaginationFilter filter, string route)
        {
            IList<VehiculoDto> vehiculosDto = null;

            int totalRecords = 0;

            if (string.IsNullOrEmpty(filter.Search))
            {
                var vehiculos = await _context.Vehiculos
                .OrderBy(vehiculos => vehiculos.Chasis)
                .Include(vehiculos => vehiculos.Transporte)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

                vehiculosDto = mapper.Map<IList<VehiculoDto>>(vehiculos);
                totalRecords = await _context.Vehiculos.CountAsync();
            }
            else
            {
                var vehiculos = await _context.Vehiculos
                .Where(vehiculos => EF.Functions.Like(vehiculos.Chasis, $"{filter.Search}%"))
                .OrderBy(vehiculos => vehiculos.Chasis)
                .Include(vehiculos => vehiculos.Transporte)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

                vehiculosDto = mapper.Map<IList<VehiculoDto>>(vehiculos);
                totalRecords = await _context.Vehiculos.Where(vehiculos => EF.Functions.Like(vehiculos.Chasis, $"{filter.Search}%")).CountAsync();
            }
                
            var pagedResponse = PaginationHelper.CreatePagedReponse(vehiculosDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
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
