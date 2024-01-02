using AutoMapper;
using Raythos.DTOs;
using Raythos.DTOs.Aircrafts;
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
            CreateMap<Aircraft, AircraftDto>();
            CreateMap<AircraftDto, Aircraft>();
            CreateMap<AircraftPostDto, Aircraft>();
            CreateMap<Aircraft, AircraftPostDto>();
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();
            CreateMap<TeamMember, TeamMemberDto>();
            CreateMap<TeamMemberDto, TeamMember>();
            CreateMap<AircraftOption, AircraftOptionDto>();
            CreateMap<AircraftOptionDto, AircraftOption>();
        }
    }
}
