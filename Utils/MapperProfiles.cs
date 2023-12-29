using AutoMapper;
using Raythos.DTOs;
using Raythos.Models;

namespace Raythos.Utils
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
        }
    }
}
