using System.ComponentModel.DataAnnotations;

namespace Raythos.Models
{
    public class UserUpdate
    {
        [Required]
        public Int64 UserId { get; set; }

        [Required]
        public string FName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public string ContactNo { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
