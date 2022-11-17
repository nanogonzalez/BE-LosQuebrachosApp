using BE_LosQuebrachosApp.Data;
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
        private readonly IUriService uriService;
        public ClienteRepository(ApplicationDbContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        public async Task<Cliente> AddCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<PagedResponse<List<Cliente>>> GetListCliente(PaginationFilter filter, string route)
        {
            
            var pagedData = await _context.Clientes
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Clientes.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, totalRecords, uriService, route);
            return pagedReponse;
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
