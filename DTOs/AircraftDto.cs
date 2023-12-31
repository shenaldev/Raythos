using Raythos.Models;
using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs
{
    public class AircraftDto
    {
        public long Id { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public string? EngineType { get; set; }

        [Required]
        public decimal? MaxSpeed { get; set; }

        [Required]
        public decimal? FuelCapacity { get; set; }

        [Required]
        public decimal? BasePrice { get; set; }

        [Required]
        public decimal? MaxPrice { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; } = "";

        [Required]
        public long? TeamId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Team? Team { get; set; }
    }
}
