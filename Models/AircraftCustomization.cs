using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythos.Models
{
    public class AircraftCustomization
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Value { get; set; } = null!;

        [Required]
        [ForeignKey("Aircraft")]
        public long AircraftId { get; set; }

        public Aircraft Aircraft { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
