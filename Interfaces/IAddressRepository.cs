using Raythos.DTOs.AddressDtos;
using Raythos.Models;

namespace Raythos.Interfaces
{
    public interface IAddressRepository
    {
        Task<ICollection<AddressDto>> GetAddresses(long userId);

        Task<Address?> GetAddress(long id);

        Task<AddressDto?> CreateAddress(AddressDto address);

        Task<AddressDto?> UpdateAddress(long id, UpdateAddressDto address);

        Task<bool> DeleteAddress(long id);

        Task<bool> IsAddressExists(long id);

        Task<bool> IsAddressBelongsToUser(long id, long userId);
    }
}
