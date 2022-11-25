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
    public class ClienteRepository: IClienteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUriService _uriService;
        private readonly IMapper mapper;
        public ClienteRepository(ApplicationDbContext context, IUriService uriService, IMapper mapper)
        {
            _context = context;
            _uriService = uriService;
            this.mapper = mapper;
        }

        public async Task<Cliente> AddCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<PagedResponse<IList<ClienteDto>>> GetListCliente(PaginationFilter filter, string route)
        {
            
            var clientes = await _context.Clientes
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var clientesDto = mapper.Map<IList<ClienteDto>>(clientes);
            var totalRecords = await _context.Clientes.CountAsync();
            var pagedResponse = PaginationHelper.CreatePagedReponse(clientesDto, filter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<Cliente> GetCliente(int id)
        {
            return await _context.Clientes.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteCliente(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
       
        public async Task UpdateCliente(Cliente cliente)
        {
            var clienteItem = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == cliente.Id);

            if (clienteItem != null)
            {
                clienteItem.Id = cliente.Id;
                clienteItem.RazonSocial = cliente.RazonSocial;
                clienteItem.Cuit = cliente.Cuit;
                clienteItem.DestinoCarga = cliente.DestinoCarga;

                await _context.SaveChangesAsync();
            }
        }
    }
}
