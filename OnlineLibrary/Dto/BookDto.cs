namespace OnlineLibrary.Dto
{
    public class BookDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Author { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int PublishedYear { get; set; }
        public List<BookRatingDto> bookRatingDtos { get; set; } = new();
    }
}
