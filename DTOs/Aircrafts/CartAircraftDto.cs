namespace Raythos.DTOs.Aircrafts
{
    public class CartAircraftDto
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
        public string? Slug { get; set; }
    }
}
