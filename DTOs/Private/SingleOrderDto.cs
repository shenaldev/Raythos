using Raythos.DTOs.AddressDtos;
using Raythos.Models;

namespace Raythos.DTOs.Private
{
    public class SingleOrderDto
    {
        public long Id { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; }

        public long? UserId { get; set; }

        public long? AddressId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UserDto User { get; set; } = null!;
        public AddressDto Address { get; set; } = null!;
        public ICollection<OrderItemDto> OrderItems { get; set; } = null!;
    }
}
