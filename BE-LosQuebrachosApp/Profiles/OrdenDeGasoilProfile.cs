using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Profiles
{
    public class OrdenDeGasoilProfile: Profile
    {
        public OrdenDeGasoilProfile()
        {
            CreateMap<OrdenDeGasoil, OrdenDeGasoilDto>();
            CreateMap<OrdenDeGasoilDto, OrdenDeGasoil>();
        }
    }
}
