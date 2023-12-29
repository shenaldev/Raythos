﻿using AutoMapper;
using Raythos.DTOs;
using Raythos.Models;
using Raythos.Responses;

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
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();
            CreateMap<TeamMember, TeamMemberDto>();
            CreateMap<TeamMemberDto, TeamMember>();
        }
    }
}
