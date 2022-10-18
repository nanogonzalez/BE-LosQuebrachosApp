using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Profiles
{
    public class ChoferProfile: Profile
    {
        public ChoferProfile()
        {
            CreateMap<Chofer, ChoferDto>();
            CreateMap<ChoferDto, Chofer>();
        }
    }
}
