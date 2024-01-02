using Raythos.Models;

namespace Raythos.DTOs.Aircrafts
{
    public class AircraftSingleDto
    {
        public long Id { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public string? EngineType { get; set; }
        public decimal? MaxSpeed { get; set; }
        public decimal? FuelCapacity { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; }
        public long? TeamId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Team? Team { get; set; }
        public ICollection<AircraftOptionDto>? AircraftOptions { get; set; }
    }
}
