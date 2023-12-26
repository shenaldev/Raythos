namespace Raythos.DTOs
{
    public class UserDto
    {
        public long Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class UserDtoPaginated
    {
        public ICollection<UserDto> Users { get; set; } = null!;
        public int Total { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }

        public UserDtoPaginated(ICollection<UserDto> users, int total, int page, int lastPage)
        {
            Users = users;
            Total = total;
            Page = page;
            LastPage = lastPage;
        }
    }
}
