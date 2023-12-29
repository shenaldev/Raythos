using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class AddressRepository : IAddressInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<AddressDto> GetAddresses(long userId)
        {
            ICollection<Address> addresses = _context.Addresses
                .Where(a => a.UserId == userId)
                .ToList();
            return _mapper.Map<ICollection<AddressDto>>(addresses);
        }

        public Address GetAddress(long id)
        {
            var address = _context.Addresses
                .Where(a => a.AddressID == id)
                .Include(b => b.Country)
                .FirstOrDefault();

            if (address == null)
            {
                return null;
            }

            return address;
        }

        public AddressDto CreateAddress(AddressDto address)
        {
            try
            {
                Address newAddress = _mapper.Map<Address>(address);
                _context.Addresses.Add(newAddress);
                _context.SaveChanges();
                return address;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateAddress(long id, AddressDto address)
        {
            try
            {
                Address updatedAddress = _mapper.Map<Address>(address);
                _context.Addresses.Update(updatedAddress);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAddress(long id)
        {
            Address? address = _context.Addresses.Find(id);
            if (address == null)
                return false;

            try
            {
                _context.Addresses.Remove(address);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsAddressExists(long id)
        {
            return _context.Addresses.Any(a => a.AddressID == id);
        }

        public bool IsAddressBelongsToUser(long id, long userId)
        {
            return _context.Addresses.Any(a => a.AddressID == id && a.UserId == userId);
        }
    }
}
