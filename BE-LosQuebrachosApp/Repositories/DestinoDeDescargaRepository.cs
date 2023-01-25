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
    public class DestinoDeDescargaRepository : IDestinoDeDescargaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper mapper;
        public DestinoDeDescargaRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }

        public async Task<DestinoDeDescarga> AddDestinoDeDescarga(DestinoDeDescarga destinoDeDescarga)
        {
            _context.DestinosDeDescarga.Add(destinoDeDescarga);
            await _context.SaveChangesAsync();
            return destinoDeDescarga;
        }

        public async Task DeleteDestinoDeDescarga(DestinoDeDescarga destinoDeDescarga)
        {
            _context.DestinosDeDescarga.Remove(destinoDeDescarga);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<IList<DestinoDeDescargaDto>>> GetListDestinoDeDescarga(PaginationFilter filter, string route)
        {
            IList<DestinoDeDescargaDto> destinoDeDescargaDto = null;

            int totalRecords = 0;

            if (string.IsNullOrEmpty(filter.Search))
            {
                var destinosDeDescarga = await _context.DestinosDeDescarga
                .OrderBy(destinosDeDescarga => destinosDeDescarga.Longitud)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

                destinoDeDescargaDto = mapper.Map<IList<DestinoDeDescargaDto>>(destinosDeDescarga);
                totalRecords = await _context.DestinosDeDescarga.CountAsync();
            }
            else
            {
                var destinosDeDescarga = await _context.DestinosDeDescarga
                .Where(destinosDeDescarga => EF.Functions.Like(destinosDeDescarga.Nombre, $"{filter.Search}%"))
                .OrderBy(destinosDeDescarga => destinosDeDescarga.Longitud)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

                destinoDeDescargaDto = mapper.Map<IList<DestinoDeDescargaDto>>(destinosDeDescarga);
                totalRecords = await _context.DestinosDeDescarga.Where(destinosDeDescarga => EF.Functions.Like(destinosDeDescarga.Nombre, $"{filter.Search}%")).CountAsync();
            }
            var pagedResponse = PaginationHelper.CreatePagedReponse(destinoDeDescargaDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<DestinoDeDescarga> GetDestinoDeDescarga(int id)
        {
            return await _context.DestinosDeDescarga.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateDestinoDeDescarga(DestinoDeDescarga destinoDeDescarga)
        {
            var destinoDeDescargaItem = await _context.DestinosDeDescarga.FirstOrDefaultAsync(x => x.Id == destinoDeDescarga.Id);

            if (destinoDeDescargaItem != null)
            {
                destinoDeDescargaItem.Longitud = destinoDeDescarga.Longitud;
                destinoDeDescargaItem.Latitud = destinoDeDescarga.Latitud;
                destinoDeDescargaItem.Nombre = destinoDeDescarga.Nombre;

                await _context.SaveChangesAsync();
            }
        }
    }
}
