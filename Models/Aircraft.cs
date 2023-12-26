using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythos.Models
{
    public class Aircraft
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Model { get; set; } = null!;

        [Required]
        [StringLength(240)]
        public string Image { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; } = null!;

        [Required]
        public DateTime ManufacturedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EngineType { get; set; } = null!;

        [Required]
        public decimal MaxSpeed { get; set; }

        [Required]
        public decimal FuelCapacity { get; set; }

        [Required]
        public decimal BasePrice { get; set; }

        [Required]
        public decimal MaxPrice { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Slug { get; set; } = null!;

        [Required]
        [ForeignKey("Team")]
        public long TeamId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Team Team { get; set; } = null!;

        public ICollection<AircraftCustomization> AircraftCustomizations { get; } =
            new List<AircraftCustomization>();
    }
}
