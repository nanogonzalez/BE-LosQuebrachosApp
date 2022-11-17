using BE_LosQuebrachosApp.Data;
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
        private readonly IUriService uriService;
        public ChoferRepository(ApplicationDbContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        public async Task<Chofer> AddChofer(Chofer chofer)
        {
            _context.Choferes.Add(chofer);
            await _context.SaveChangesAsync();
            return chofer;
        }

        public async Task DeleteChofer(Chofer chofer)
        {
            _context.Choferes.Remove(chofer);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<List<Chofer>>> GetListChoferes(PaginationFilter filter, string route)
        {
            
            var pagedData = await _context.Choferes
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Choferes.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, totalRecords, uriService, route);
            return pagedReponse;
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
