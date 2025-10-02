using OnlineLibrary.Dto;
using OnlineLibrary.Model;

namespace OnlineLibrary.Repository
{
    public   interface IBookRepositroy
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<BookDto?> GetBookById(int id);
        Task AddBookAsync(AddBookDto book);
        Task EditBook( int id ,EditBookDto book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
    }
}
