namespace Raythos.Responses
{
    public class TeamResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<TeamMemberResponse> TeamMembers { get; set; } = null!;
    }

    public class TeamMemberResponse
    {
        public long? TeamId { get; set; }
        public long UserId { get; set; }

        public UserResponse User { get; set; } = null!;
    }

    public class UserResponse
    {
        public long Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
