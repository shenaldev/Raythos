using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Raythos.Models
{
    public class TeamMember
    {
        [Key, Column(Order = 0)]
        public long TeamId { get; set; }

        [Key, Column(Order = 1)]
        public long UserId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public Team Team { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
