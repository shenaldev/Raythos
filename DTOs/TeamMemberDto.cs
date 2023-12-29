using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs
{
    public class TeamMemberDto
    {
        [Required]
        public long? TeamId { get; set; }

        [Required]
        public long UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
