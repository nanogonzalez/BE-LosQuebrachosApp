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
    public class DestinoDeCargaRepository : IDestinoDeCargaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper mapper;
        public DestinoDeCargaRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }

        public async Task<DestinoDeCarga> AddDestinoDeCarga(DestinoDeCarga destinoDeCarga)
        {
            destinoDeCarga.Cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == destinoDeCarga.Cliente.Id);
            _context.DestinosDeCarga.Add(destinoDeCarga);
            await _context.SaveChangesAsync();
            return destinoDeCarga;
        }

        public async Task<PagedResponse<IList<DestinoDeCargaDto>>> GetDestinoDeCargaByCliente(PaginationFilter filter, string route, int idCliente)
        {
            IList<DestinoDeCargaDto> destinosDeCargaDto = null;

            int totalRecords = 0;

            if (string.IsNullOrEmpty(filter.Search))
            {
                var destinosDeCarga = await _context.DestinosDeCarga
                    .Where(destinosDeCarga => destinosDeCarga.Cliente.Id == idCliente)
                    .OrderBy(destinosDeCarga => destinosDeCarga.Cliente.RazonSocial)
                    .Include(destinosDeCarga => destinosDeCarga.Cliente)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                destinosDeCargaDto = mapper.Map<IList<DestinoDeCargaDto>>(destinosDeCarga);
                totalRecords = await _context.DestinosDeCarga.CountAsync();
            }
            else
            {
                var destinosDeCarga = await _context.DestinosDeCarga
                    .Where(destinosDeCarga => EF.Functions.Like(destinosDeCarga.Cliente.RazonSocial, $"{filter.Search}%") && destinosDeCarga.Cliente.Id == idCliente)
                    .OrderBy(destinosDeCarga => destinosDeCarga.Cliente.RazonSocial)
                    .Include(destinosDeCarga => destinosDeCarga.Cliente)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                destinosDeCargaDto = mapper.Map<IList<DestinoDeCargaDto>>(destinosDeCarga);
                totalRecords = await _context.DestinosDeCarga.Where(destinosDeCarga => EF.Functions.Like(destinosDeCarga.Cliente.RazonSocial, $"{filter.Search}%") && destinosDeCarga.Cliente.Id == idCliente).CountAsync();
            }

            var pagedResponse = PaginationHelper.CreatePagedReponse(destinosDeCargaDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<PagedResponse<IList<DestinoDeCargaDto>>> GetListDestinoDeCarga(PaginationFilter filter, string route)
        {
            IList<DestinoDeCargaDto> destinosDeCargaDto = null;

            int totalRecords = 0;

            if (string.IsNullOrEmpty(filter.Search))
            {
                var destinosDeCarga = await _context.DestinosDeCarga
                    .OrderBy(destinosDeCarga => destinosDeCarga.Cliente.RazonSocial)
                    .Include(destinosDeCarga => destinosDeCarga.Cliente)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                destinosDeCargaDto = mapper.Map<IList<DestinoDeCargaDto>>(destinosDeCarga);
                totalRecords = await _context.DestinosDeCarga.CountAsync();
            }
            else
            {
                var destinosDeCarga = await _context.DestinosDeCarga
                    .Where(destinosDeCarga => EF.Functions.Like(destinosDeCarga.Cliente.RazonSocial, $"{filter.Search}%"))
                    .OrderBy(destinosDeCarga => destinosDeCarga.Cliente.RazonSocial)
                    .Include(destinosDeCarga => destinosDeCarga.Cliente)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();

                destinosDeCargaDto = mapper.Map<IList<DestinoDeCargaDto>>(destinosDeCarga);
                totalRecords = await _context.DestinosDeCarga.Where(destinosDeCarga => EF.Functions.Like(destinosDeCarga.Cliente.RazonSocial, $"{filter.Search}%")).CountAsync();
            }

            var pagedResponse = PaginationHelper.CreatePagedReponse(destinosDeCargaDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<DestinoDeCarga> GetDestinoDeCarga(int id)
        {
            
            return await _context.DestinosDeCarga
                .Include(destinosDeCarga => destinosDeCarga.Cliente)
                .Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteDestinoDeCarga(DestinoDeCarga destinoDeCarga)
        {
            _context.DestinosDeCarga.Remove(destinoDeCarga);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDestinoDeCarga(DestinoDeCarga destinoDeCarga)
        {
            var destinoDeCargaItem = await _context.DestinosDeCarga.FirstOrDefaultAsync(x => x.Id == destinoDeCarga.Id);

            if (destinoDeCargaItem != null)
            {
                destinoDeCargaItem.Id = destinoDeCarga.Id;
                destinoDeCargaItem.Longitud = destinoDeCarga.Longitud;
                destinoDeCargaItem.Latitud = destinoDeCarga.Latitud;
                destinoDeCargaItem.NombreEstablecimiento = destinoDeCarga.NombreEstablecimiento;
                destinoDeCargaItem.Cliente = destinoDeCarga.Cliente;

                await _context.SaveChangesAsync();
            }
        }
    }
}
