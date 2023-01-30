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
    public class OrdenDeGasoilRepository: IOrdenDeGasoilRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper mapper;
       
        public OrdenDeGasoilRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }
    
        public async Task<OrdenDeGasoil> AddOrdenDeGasoil(OrdenDeGasoil ordenDeGasoil)
        {
            ordenDeGasoil.NumeroOrden = await GenerarNumeroOrden();
            ordenDeGasoil.Transporte = await _context.Transportes.FirstOrDefaultAsync(x => x.Id == ordenDeGasoil.Transporte.Id);
            ordenDeGasoil.Chofer = await _context.Choferes.FirstOrDefaultAsync(x => x.Id == ordenDeGasoil.Chofer.Id);
            ordenDeGasoil.Vehiculo = await _context.Vehiculos.FirstOrDefaultAsync(x => x.Id == ordenDeGasoil.Vehiculo.Id);
            _context.OrdenesDeGasoil.Add(ordenDeGasoil);
            await _context.SaveChangesAsync();
            return ordenDeGasoil; 
        }

        public async Task DeleteOrdenDeGasoil(OrdenDeGasoil ordenDeGasoil)
        {
            _context.OrdenesDeGasoil.Remove(ordenDeGasoil);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<IList<OrdenDeGasoilDto>>> GetListOrdenDeGasoil(PaginationFilter filter, string route)
        {
            IList<OrdenDeGasoilDto> ordenesDeGasoilDto = null;

            int totalRecords = 0;

            if (string.IsNullOrEmpty(filter.Search))
            {
                var ordenesDeGasoil = await _context.OrdenesDeGasoil
                .OrderBy(ordenesDeGasoil => ordenesDeGasoil.NumeroOrden)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Transporte)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Chofer)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Vehiculo)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

                ordenesDeGasoilDto = mapper.Map<IList<OrdenDeGasoilDto>>(ordenesDeGasoil);
                totalRecords = await _context.OrdenesDeGasoil.CountAsync();
            }
            else
            {
                var ordenesDeGasoil = await _context.OrdenesDeGasoil
                .Where(ordenesDeGasoil=> ordenesDeGasoil.NumeroOrden.Contains(filter.Search))
                .OrderBy(ordenesDeGasoil => ordenesDeGasoil.NumeroOrden)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Transporte)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Chofer)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Vehiculo)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

                ordenesDeGasoilDto = mapper.Map<IList<OrdenDeGasoilDto>>(ordenesDeGasoil);
                totalRecords = await _context.OrdenesDeGasoil.Where(ordenesDeGasoil => ordenesDeGasoil.NumeroOrden.Contains(filter.Search)).CountAsync();
            }

            var pagedResponse = PaginationHelper.CreatePagedReponse(ordenesDeGasoilDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<OrdenDeGasoil> GetOrdenDeGasoil(int id)
        {
           return await _context.OrdenesDeGasoil
                .Include(ordenesDeGasoil => ordenesDeGasoil.Transporte)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Chofer)
                .Include(ordenesDeGasoil => ordenesDeGasoil.Vehiculo)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateOrdenDeGasoil(OrdenDeGasoil ordenDeGasoil)
        {
            var ordenDeGasoilItem = await _context.OrdenesDeGasoil.FirstOrDefaultAsync(x => x.Id == ordenDeGasoil.Id);

            if (ordenDeGasoilItem != null)
            {
                ordenDeGasoilItem.NumeroOrden = ordenDeGasoil.NumeroOrden;
                ordenDeGasoilItem.Fecha = ordenDeGasoil.Fecha;
                ordenDeGasoilItem.Transporte = ordenDeGasoil.Transporte;
                ordenDeGasoilItem.Chofer = ordenDeGasoil.Chofer;
                ordenDeGasoilItem.Vehiculo = ordenDeGasoil.Vehiculo;
                ordenDeGasoilItem.Litros = ordenDeGasoil.Litros;
                ordenDeGasoilItem.Estacion = ordenDeGasoil.Estacion;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerarNumeroOrden()
        {
            var ultimaOrden = _context.OrdenesDeGasoil.OrderByDescending(o => o.NumeroOrden).FirstOrDefault();
            var ultimoNumero = ultimaOrden != null ? int.Parse(ultimaOrden.NumeroOrden.Substring(9)) : 000000;
            return "OG-001-" + (ultimoNumero + 1).ToString("D6");
        }
    }
}
