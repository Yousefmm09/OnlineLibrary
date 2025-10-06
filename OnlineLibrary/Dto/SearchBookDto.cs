
namespace OnlineLibrary.Dto
{
    public class SearchBookDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }=string.Empty;
        public  string? CategoryName {  get; set; }= string.Empty;
        public List<BookReview> BookReviews {  get; set; } = new List<BookReview>();

    }
}
