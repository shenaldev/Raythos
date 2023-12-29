namespace Raythos.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}
