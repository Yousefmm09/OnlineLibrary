using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Dto;
using OnlineLibrary.Model;

namespace OnlineLibrary.Repository
{
    public class BookRepository : IBookRepositroy
    {
        private readonly OBDbcontext _dbcontext;
        public BookRepository(OBDbcontext bDbcontext)
        {
            _dbcontext = bDbcontext;
        }
        public async Task AddBookAsync(AddBookDto addBookDto)
        {
            var namebook = await _dbcontext.Books.FirstOrDefaultAsync(x => x.Title == addBookDto.Title);
            if (namebook == null)
            {
                var newBook = new Book
                {
                    Title = addBookDto.Title,
                    Author = addBookDto.Author,
                    Price = addBookDto.Price,
                    ISBN = addBookDto.ISBN,
                    Description = addBookDto.Description,
                    Stock = addBookDto.Stock,
                    CategoryId = addBookDto.CategoryId,
                    ImageUrl = addBookDto.ImageUrl,
                    PublishedYear = addBookDto.PublishedYear,
                };

                await _dbcontext.Books.AddAsync(newBook);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _dbcontext.Books.FindAsync(id);
            if (book != null)
            {
                _dbcontext.Books.Remove(book);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task EditBook(int id , EditBookDto bookDto)
        {
            var book = await _dbcontext.Books.FindAsync(id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.Price = bookDto.Price;
                book.ISBN = bookDto.ISBN;
                book.Stock = bookDto.Stock;
                book.PublishedYear = bookDto.PublishedYear;
                book.CategoryId = bookDto.CategoryId;
                book.Borrows = new List<Borrow>();
                await _dbcontext.SaveChangesAsync();
            }

        }

        public async Task<BookDto?> GetBookById(int id)
        {
            return await _dbcontext.Books
                .Where(x => x.Id == id)
                .Include(c => c.Category)
                .Select(x => new BookDto
                {
                    Title = x.Title,
                    Description = x.Description,
                    Author = x.Author,
                    Category = x.Category.Name,
                    Price = x.Price,
                    Stock = x.Stock,
                    PublishedYear = x.PublishedYear,
                    bookRatingDtos = x.BookReviews.Select(r => new BookRatingDto
                    {
                        Rating = r.Rating,
                        Comment = r.Comment,
                        DateTime = r.DateTime
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
