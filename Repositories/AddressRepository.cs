using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs;
using Raythos.DTOs.Address;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<AddressDto>> GetAddresses(long userId)
        {
            ICollection<Address> addresses = await _context.Addresses
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return _mapper.Map<ICollection<AddressDto>>(addresses);
        }

        public async Task<Address?> GetAddress(long id)
        {
            var address = await _context.Addresses
                .Where(a => a.AddressID == id)
                .Include(b => b.Country)
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return null;
            }

            return address;
        }

        public async Task<AddressDto?> CreateAddress(AddressDto address)
        {
            try
            {
                Address newAddress = _mapper.Map<Address>(address);
                await _context.Addresses.AddAsync(newAddress);
                await _context.SaveChangesAsync();
                return address;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AddressDto?> UpdateAddress(long id, UpdateAddressDto addressDto)
        {
            try
            {
                var exsistingAddress = await _context.Addresses.FirstOrDefaultAsync(
                    a => a.AddressID == id
                );
                if (exsistingAddress == null)
                {
                    return null;
                }

                exsistingAddress.Street = addressDto.Street;
                exsistingAddress.City = addressDto.City;
                exsistingAddress.PostalCode = addressDto.PostalCode;
                exsistingAddress.CountryId = addressDto.CountryId;

                await _context.SaveChangesAsync();
                return _mapper.Map<AddressDto>(exsistingAddress);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAddress(long id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return false;

            try
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsAddressExists(long id)
        {
            return await _context.Addresses.AnyAsync(a => a.AddressID == id);
        }

        public async Task<bool> IsAddressBelongsToUser(long id, long userId)
        {
            return await _context.Addresses.AnyAsync(a => a.AddressID == id && a.UserId == userId);
        }
    }
}
