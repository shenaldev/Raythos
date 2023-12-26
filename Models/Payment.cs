using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythos.Models
{
    public class Payment
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Method { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Completed";

        public string? TransactionId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public long OrderId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Order Order { get; set; } = null!;
    }
}
