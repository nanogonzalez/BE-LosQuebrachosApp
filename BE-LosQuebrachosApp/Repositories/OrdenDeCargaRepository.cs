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
    public class OrdenDeCargaRepository: IOrdenDeCargaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper mapper;
        public OrdenDeCargaRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }

        public async Task<OrdenDeCarga> AddOrdenDeCarga(OrdenDeCarga ordenDeCarga)
        {
            ordenDeCarga.NumeroOrden = await GenerarNumeroOrden();
            ordenDeCarga.DestinoDeCarga = await _context.DestinosDeCarga.FirstOrDefaultAsync(x => x.Id == ordenDeCarga.DestinoDeCarga.Id);
            ordenDeCarga.DestinoDeDescarga = await _context.DestinosDeDescarga.FirstOrDefaultAsync(x => x.Id == ordenDeCarga.DestinoDeDescarga.Id);
            ordenDeCarga.Cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == ordenDeCarga.Cliente.Id);
            _context.OrdenesDeCargas.Add(ordenDeCarga);
            await _context.SaveChangesAsync();
            return ordenDeCarga;
        }

        public async Task<PagedResponse<IList<OrdenDeCargaDto>>> GetListOrdenDeCarga(PaginationFilter filter, string route)
        {
            IList<OrdenDeCargaDto> ordenesDeCargasDto = null;

            int totalRecords = 0;

            if (string.IsNullOrEmpty(filter.Search)){

                var ordenesDeCargas = await _context.OrdenesDeCargas
                  .OrderBy(ordenesDeCargas => ordenesDeCargas.NumeroOrden)
                  .Include(ordenesDeCargas => ordenesDeCargas.DestinoDeCarga)
                  .Include(ordenesDeCargas => ordenesDeCargas.DestinoDeDescarga)
                  .Include(ordenesDeCargas => ordenesDeCargas.Cliente)
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize)
                  .ToListAsync();

                ordenesDeCargasDto = mapper.Map<IList<OrdenDeCargaDto>>(ordenesDeCargas);
                totalRecords = await _context.OrdenesDeCargas.CountAsync();
            }
            else
            {
                var ordenesDeCargas = await _context.OrdenesDeCargas
                  .Where(ordenesDeCargas => ordenesDeCargas.NumeroOrden.Contains(filter.Search))
                  .OrderBy(ordenesDeCargas => ordenesDeCargas.Cliente.RazonSocial)
                  .Include(ordenesDeCargas => ordenesDeCargas.DestinoDeCarga)
                  .Include(ordenesDeCargas => ordenesDeCargas.DestinoDeDescarga)
                  .Include(ordenesDeCargas => ordenesDeCargas.Cliente)
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize)
                  .ToListAsync();

                ordenesDeCargasDto = mapper.Map<IList<OrdenDeCargaDto>>(ordenesDeCargas);
                totalRecords = await _context.OrdenesDeCargas.Where(ordenesDeCargas => ordenesDeCargas.NumeroOrden.Contains(filter.Search)).CountAsync();
            }
                
            var pagedResponse = PaginationHelper.CreatePagedReponse(ordenesDeCargasDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<OrdenDeCarga> GetOrdenDeCarga(int id)
        {
            return await _context.OrdenesDeCargas
                .Include(ordenesDeCargas => ordenesDeCargas.DestinoDeCarga)
                .Include(ordenesDeCargas => ordenesDeCargas.DestinoDeDescarga)
                .Include(ordenesDeCargas => ordenesDeCargas.Cliente)
                .Where(a => a.Id == id).FirstOrDefaultAsync();
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
                ordenDeCargaItem.NumeroOrden = ordenDeCarga.NumeroOrden;
                ordenDeCargaItem.DestinoDeCarga = ordenDeCarga.DestinoDeCarga;
                ordenDeCargaItem.DestinoDeDescarga = ordenDeCarga.DestinoDeDescarga;
                ordenDeCargaItem.DistanciaViaje = ordenDeCarga.DistanciaViaje;
                ordenDeCargaItem.DiaHoraCarga = ordenDeCarga.DiaHoraCarga; 
                ordenDeCargaItem.TipoMercaderia = ordenDeCarga.TipoMercaderia;
                ordenDeCargaItem.Cliente = ordenDeCarga.Cliente;    

                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerarNumeroOrden()
        {
            var ultimaOrden = _context.OrdenesDeCargas.OrderByDescending(o => o.NumeroOrden).FirstOrDefault();
            var ultimoNumero = ultimaOrden != null ? int.Parse(ultimaOrden.NumeroOrden.Substring(9)) : 000000;
            return "OC-001-" + (ultimoNumero + 1).ToString("D6");
        }
    }
}

