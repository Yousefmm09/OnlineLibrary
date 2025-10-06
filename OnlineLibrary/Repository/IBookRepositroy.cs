namespace OnlineLibrary.Repository
{
    public   interface IBookRepositroy
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<BookDto?> GetBookById(int id);
        Task AddBookAsync(AddBookDto book);
        Task EditBook( int id ,EditBookDto book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<SearchBookDto>> SearchBooksAsync(SearchBookDto searchTerm);
        Task <object> GetPaginationParametersAsync(PaginationParameters pagination);
        Task<BookRatingDto> GetRatingParametersAsync(string userId,BookRatingDto bookRating);
    }
}
