using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs
{
    public class AircraftOptionDto
    {
        public long Id { get; set; }
        public string Key { get; set; } = null!;
        public string Values { get; set; } = null!;

        [Required]
        public decimal? Price { get; set; }

        public long? AircraftId { get; set; }
    }
}
