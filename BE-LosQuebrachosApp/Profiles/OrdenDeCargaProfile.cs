using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Profiles
{
    public class OrdenDeCargaProfile: Profile
    {
        public OrdenDeCargaProfile()
        {
            CreateMap<OrdenDeCarga, OrdenDeCargaDto>();
            CreateMap<OrdenDeCargaDto, OrdenDeCarga>();
        }
    }
}
