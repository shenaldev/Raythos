using AutoMapper;
using Raythos.DTOs;
using Raythos.DTOs.Address;
using Raythos.DTOs.Aircrafts;
using Raythos.DTOs.Categories;
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
            CreateMap<Address, UpdateAddressDto>();
            CreateMap<UpdateAddressDto, Address>();

            //Categories Mapping
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();

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

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, CreateOrderDto>();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, CreateOrderItemDto>();
            CreateMap<CreateOrderItemDto, OrderItem>();
        }
    }
}
