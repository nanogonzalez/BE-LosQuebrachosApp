using AutoMapper;
using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Helpers;
using BE_LosQuebrachosApp.Services;
using BE_LosQuebrachosApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Repositories
{
    public class TransporteRepository: ITransporteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper mapper;

        public TransporteRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }
        
        public async Task<Transporte> AddTransporte(Transporte transporte)
        {
            _context.Transportes.Add(transporte);
            await _context.SaveChangesAsync();  
            return transporte;
        }

        public async Task DeleteTransporte(Transporte transporte)
        {
            _context.Transportes.Remove(transporte);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<IList<TransporteDto>>> GetListTransportes(PaginationFilter filter, string route)
        {
            var transportes = await _context.Transportes
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var transportesDto = mapper.Map<IList<TransporteDto>>(transportes);

            var totalRecords = await _context.Transportes.CountAsync();
            var pagedResponse = PaginationHelper.CreatePagedReponse(transportesDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<Transporte> GetTransporte(int id)
        {
            return await _context.Transportes.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateTransporte(Transporte transporte)
        {
            var transporteItem = await _context.Transportes.FirstOrDefaultAsync(x => x.Id == transporte.Id);

            if (transporteItem != null)
            {
                transporteItem.Nombre = transporte.Nombre;
                transporteItem.Apellido = transporte.Apellido;
                transporteItem.Cuit = transporte.Cuit;

                await _context.SaveChangesAsync();
            }
        }
    }
}
