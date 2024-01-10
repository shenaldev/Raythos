using Raythos.DTOs.Aircrafts;

namespace Raythos.DTOs.Private
{
    public class SingleOrderItemDto
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long AircraftId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Customizations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public AircraftDto? Aircraft { get; set; }
    }
}
