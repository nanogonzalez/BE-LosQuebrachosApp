using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Profiles
{
    public class DestinoDeCargaProfile : Profile
    {
        public DestinoDeCargaProfile()
        {
            CreateMap<DestinoDeCarga, DestinoDeCargaDto>();
            CreateMap<DestinoDeCargaDto, DestinoDeCarga>();
        }
    }
}
