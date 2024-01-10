using Raythos.DTOs.AddressDtos;

namespace Raythos.DTOs.Private.OrderDtos
{
    public class OrderDto
    {
        public long Id { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Pending";
        public long? UserId { get; set; }
        public long? AddressId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UserDto? User { get; set; }
        public AddressDto? Address { get; set; }
    }
}
