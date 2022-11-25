using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Wrappers;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IVehiculoRepsitory
    {
        Task DeleteVehiculo(Vehiculo vehiculo);
        Task<Vehiculo> AddVehiculo(Vehiculo vehiculo);
        Task<PagedResponse<IList<VehiculoDto>>> GetListVehiculos(PaginationFilter filter, string route);
        Task<Vehiculo> GetVehiculo(int id);
        Task UpdateVehiculo(Vehiculo vehiculo);
    }    
}
