using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythos.Models
{
    public class AircraftOption
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; } = null!;

        [Required]
        public string Values { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Aircraft")]
        public long AircraftId { get; set; }

        public Aircraft Aircraft { get; set; } = null!;
    }
}
