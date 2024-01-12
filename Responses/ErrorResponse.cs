namespace Raythos.Responses
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }

        public ErrorResponse() { }

        public static ErrorResponse CreateValidationError(Dictionary<string, List<string>> errors)
        {
            return new ErrorResponse
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                Title = "One or more validation errors occurred.",
                Status = 400,
                Errors = errors,
            };
        }
    }
}
