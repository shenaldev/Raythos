using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs.Private
{
    public class CreateOrderDto
    {
        public long Id { get; set; }

        [Required]
        public decimal? Total { get; set; }
        public string Status { get; set; } = "Pending";

        [Required]
        public long? UserId { get; set; }

        [Required]
        public long? AddressId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
