namespace Raythos.Responses
{
    public class Meta
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
    }

    public class PaginatedResponse
    {
        public required object Data { get; set; }
        public required Meta Meta { get; set; }
    }
}
