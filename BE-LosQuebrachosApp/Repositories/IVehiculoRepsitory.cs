using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Repositories
{
    public interface IVehiculoRepsitory
    {
        Task DeleteVehiculo(Vehiculo vehiculo);
        Task<Vehiculo> AddVehiculo(Vehiculo vehiculo);
        Task<List<Vehiculo>> GetListVehiculos();
        Task<Vehiculo> GetVehiculo(int id);
        Task UpdateVehiculo(Vehiculo vehiculo);
    }    
}
