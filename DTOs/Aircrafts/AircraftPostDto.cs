using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs.Aircrafts
{
    public class AircraftPostDto
    {
        public long Id { get; set; }
        public string Model { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string ManufacturedDate { get; set; } = null!;
        public string? EngineType { get; set; }

        [Required]
        public decimal? MaxSpeed { get; set; }

        [Required]
        public decimal? FuelCapacity { get; set; }

        [Required]
        public decimal? BasePrice { get; set; }

        [Required]
        public decimal? MaxPrice { get; set; }
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Slug { get; set; } = "";

        [Required]
        public long? TeamId { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
