using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Raythos.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [ForeignKey("User")]
        public long? UserId { get; set; }

        [ForeignKey("Address")]
        public long? AddressId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; } = null!;
        public Address Address { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = null!;
    }
}
