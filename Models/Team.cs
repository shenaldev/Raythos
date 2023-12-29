using System.ComponentModel.DataAnnotations;

namespace Raythos.Models
{
    public class Team
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public DateTime? CreatedAt { get; set; } = new DateTime();
        public DateTime? UpdatedAt { get; set; }

        public ICollection<TeamMember> TeamMembers { get; } = new List<TeamMember>();

        public Aircraft Aircraft { get; set; } = null!;
    }
}
