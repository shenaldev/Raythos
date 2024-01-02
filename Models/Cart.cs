using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythos.Models
{
    public class Cart
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [ForeignKey("Aircraft")]
        public long AircraftId { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
        public string? Customizations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Aircraft Aircraft { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
