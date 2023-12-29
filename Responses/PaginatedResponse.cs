namespace Raythos.Responses
{
    public class PaginatedResponse<T>
    {
        public ICollection<T> Data { get; set; } = null!;
        public int TotalResults { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }

        public static PaginatedResponse<T> Paginate(
            ICollection<T> data,
            int totalResults,
            int page,
            int lastPage
        )
        {
            return new PaginatedResponse<T>
            {
                Data = data,
                TotalResults = totalResults,
                Page = page,
                LastPage = lastPage
            };
        }
    }
}
