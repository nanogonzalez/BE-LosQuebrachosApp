using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Profiles
{
    public class DestinoDeDescargaProfile : Profile
    {
        public DestinoDeDescargaProfile()
        {
            CreateMap<DestinoDeDescarga, DestinoDeDescargaDto>();
            CreateMap<DestinoDeDescargaDto, DestinoDeDescarga>();
        }
    }
}
