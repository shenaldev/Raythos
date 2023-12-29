using Raythos.DTOs;
using Raythos.Models;

namespace Raythos.Interfaces
{
    public interface IAddressInterface
    {
        ICollection<AddressDto> GetAddresses(long userId);

        Address GetAddress(long id);

        AddressDto CreateAddress(AddressDto address);

        bool UpdateAddress(long id, AddressDto address);

        bool DeleteAddress(long id);

        bool IsAddressExists(long id);

        bool IsAddressBelongsToUser(long id, long userId);
    }
}
