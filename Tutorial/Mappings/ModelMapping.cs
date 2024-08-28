using AutoMapper;
using IntergrateMongodb.Dtos;
using IntergrateMongodb.Models;

namespace IntergrateMongodb.Mappings
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
        }
    }
}
