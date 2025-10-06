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
                .Include(r => r.BookReviews)
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
                        BookId = r.BookId,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        DateTime = r.DateTime
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<object> GetPaginationParametersAsync(PaginationParameters pagination)
        {
            var query = _dbcontext.Books.AsQueryable();
            var count = await query.CountAsync();

            var book = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            var result = new
            {
                TotalCount = count,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pagination.PageSize),
                Data = book
            };
            return result;
        }

        public async Task<IEnumerable<SearchBookDto>> SearchBooksAsync(SearchBookDto searchTerm)
        {
            var book= await _dbcontext.Books.Where(x=>x.Title.Contains(searchTerm.Title)
            ||x.Price.Equals(searchTerm.Price)||x.Author.Contains(searchTerm.Author))
                .Select(x=> new SearchBookDto
                {
                   Title= x.Title,
                   Price= x.Price, 
                    Author= x.Author,
                    Description = x.Description,
                    CategoryName=x.Category.Name,
                    BookReviews=x.BookReviews.ToList()
                }).ToListAsync();
            return book;
        }

        public async Task<BookRatingDto> GetRatingParametersAsync(string userId,BookRatingDto bookRating)
        {
            var book =  await _dbcontext.Books.FindAsync(bookRating.BookId);
            if (book == null)
                throw new Exception("Book not found");

            var rating = new BookReview
            {
                Rating = bookRating.Rating,
                Comment = bookRating.Comment,
                DateTime = bookRating.DateTime,
                BookId = bookRating.BookId,
                UserId=userId
            };
            await _dbcontext.BookReviews.AddAsync(rating);
            await _dbcontext.SaveChangesAsync();

            var result = new BookRatingDto
            {
                Rating = rating.Rating,
                Comment = rating.Comment,
                DateTime = rating.DateTime,
                BookId = rating.BookId
            };
            return result;
        }
    }
}
