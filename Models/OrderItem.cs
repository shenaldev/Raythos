using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Raythos.Models
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [ForeignKey("Order")]
        public long OrderId { get; set; }

        [Required]
        [ForeignKey("Aircraft")]
        public long AircraftId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
        public string? Customizations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public Order Order { get; set; } = null!;

        [JsonIgnore]
        public Aircraft Aircraft { get; set; } = null!;
    }
}
