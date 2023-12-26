namespace Raythos.DTOs
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        public DateTime? UpdatedAt { get; set; }
    }
}
