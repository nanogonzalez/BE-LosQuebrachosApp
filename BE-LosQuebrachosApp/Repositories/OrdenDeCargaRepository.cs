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
            _context.OrdenesDeCargas.Add(ordenDeCarga);
            await _context.SaveChangesAsync();
            return ordenDeCarga;
        }

        public async Task<PagedResponse<IList<OrdenDeCargaDto>>> GetListOrdenDeCarga(PaginationFilter filter, string route)
        {

            var ordenesDeCargas = await _context.OrdenesDeCargas
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var ordenesDeCargasDto = mapper.Map<IList<OrdenDeCargaDto>>(ordenesDeCargas);
            var totalRecords = await _context.OrdenesDeCargas.CountAsync();
            var pagedResponse = PaginationHelper.CreatePagedReponse(ordenesDeCargasDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<OrdenDeCarga> GetOrdenDeCarga(int id)
        {
            return await _context.OrdenesDeCargas.Where(a => a.Id == id).FirstOrDefaultAsync();
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

