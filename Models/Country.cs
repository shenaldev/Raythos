using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Raythos.Models
{
    public class Country
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(3)]
        public required string Code { get; set; }

        [JsonIgnore]
        public ICollection<Address> Addresses { get; } = new List<Address>();
    }
}
