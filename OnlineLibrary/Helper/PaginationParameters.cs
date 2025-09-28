using OnlineLibrary.Data;

namespace OnlineLibrary.Helper
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int MaxPageSize { get; set; } = 50;
        public int GetValidParameter()
        {
            return (PageSize > MaxPageSize) ? MaxPageSize : PageSize;
        }

    }
}
