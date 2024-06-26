using AutoMapper;
using Pizzeria.Dto;
using Pizzeria.Model;

namespace Pizzeria.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewUserDtoReq, User>();

        }
    }
}