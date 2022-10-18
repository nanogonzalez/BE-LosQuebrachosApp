using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Profiles
{
    public class TransporteProfile: Profile
    {
        public TransporteProfile()
        {
            CreateMap<Transporte, TransporteDto>();
            CreateMap<TransporteDto, Transporte>();
        }
    }
}
