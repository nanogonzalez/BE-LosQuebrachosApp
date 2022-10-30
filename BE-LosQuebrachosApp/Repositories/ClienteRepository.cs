using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE_LosQuebrachosApp.Repositories
{
    public class ClienteRepository: IClienteRepository
    {
        private readonly ApplicationDbContext _context;
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<Cliente> AddCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();  
            return cliente;
        }

        public async Task<List<Cliente>> GetListClientes()
        {
            return await _context.Clientes.ToListAsync();
        } 

        public async Task<Cliente> GetCliente(int id)
        {
            return await _context.Clientes.FindAsync(id);
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
                clienteItem.DestinoCarga = cliente.DestinoCarga;
                clienteItem.DestinoDescarga = cliente.DestinoDescarga;
                clienteItem.DiaCarga = cliente.DiaCarga;
                clienteItem.HoraCarga = cliente.HoraCarga;
                clienteItem.TipoMercaderia = cliente.TipoMercaderia;

                await _context.SaveChangesAsync();
            }
        }
    }
}
