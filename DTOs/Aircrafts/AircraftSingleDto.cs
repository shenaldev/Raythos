using Raythos.Models;

namespace Raythos.DTOs.Aircrafts
{
    public class AircraftSingleDto
    {
        public long Id { get; set; }
        public string Model { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public DateTime ManufacturedDate { get; set; }
        public string? EngineType { get; set; }
        public decimal? MaxSpeed { get; set; }
        public decimal? FuelCapacity { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Slug { get; set; }
        public long? TeamId { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Category? Category { get; set; }
        public Team? Team { get; set; }
        public ICollection<AircraftOptionDto>? AircraftOptions { get; set; }
    }
}
