namespace Raythos.Responses
{
    public class LoginResponse
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
