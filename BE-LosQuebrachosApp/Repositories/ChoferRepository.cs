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
    public class ChoferRepository: IChoferRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper mapper;
        public ChoferRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }

        public async Task<Chofer> AddChofer(Chofer chofer)
        {
            chofer.Transporte = await _context.Transportes.FirstOrDefaultAsync(x => x.Id == chofer.Transporte.Id);
            _context.Choferes.Add(chofer);
            await _context.SaveChangesAsync();
            return chofer;
        }

        public async Task DeleteChofer(Chofer chofer)
        {
            _context.Choferes.Remove(chofer);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<IList<ChoferDto>>> GetListChoferes(PaginationFilter filter, string route)
        {
            
            var choferes = await _context.Choferes
                .Include(choferes => choferes.Transporte)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var choferesDto = mapper.Map<IList<ChoferDto>>(choferes);
            var totalRecords = await _context.Choferes.CountAsync();
            var pagedResponse = PaginationHelper.CreatePagedReponse(choferesDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<Chofer> GetChofer(int id)
        {
            return await _context.Choferes.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateChofer(Chofer chofer)
        {
            var choferItem = await _context.Choferes.FirstOrDefaultAsync(x => x.Id == chofer.Id);

            if (choferItem != null)
            {
                choferItem.Nombre = chofer.Nombre;
                choferItem.Apellido = chofer.Apellido;
                choferItem.Cuit = chofer.Cuit;
                
                await _context.SaveChangesAsync();
            }
        }
    }

}
