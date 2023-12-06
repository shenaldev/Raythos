namespace Raythos.Responses
{
    public class LoginResponse
    {
        public Int64 UserID { get; set; }
        public required string FName { get; set; }
        public required string LName { get; set; }
        public required string Email { get; set; }
        public required bool IsAdmin { get; set; }
        public string? Token { get; set; }
    }
}
