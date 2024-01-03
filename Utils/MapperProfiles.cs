using AutoMapper;
using Raythos.DTOs;
using Raythos.DTOs.Aircrafts;
using Raythos.DTOs.Private;
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

            // Aircrafts
            CreateMap<Aircraft, AircraftDto>();
            CreateMap<AircraftDto, Aircraft>();
            CreateMap<Aircraft, AircraftSingleDto>();
            CreateMap<AircraftPostDto, Aircraft>();
            CreateMap<Aircraft, AircraftPostDto>();
            CreateMap<Aircraft, CartAircraftDto>();

            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();
            CreateMap<TeamMember, TeamMemberDto>();
            CreateMap<TeamMemberDto, TeamMember>();
            CreateMap<AircraftOption, AircraftOptionDto>();
            CreateMap<AircraftOptionDto, AircraftOption>();

            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();
            CreateMap<Cart, CreateCartDto>();
            CreateMap<CreateCartDto, Cart>();
        }
    }
}
