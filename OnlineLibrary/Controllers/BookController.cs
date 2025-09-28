using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Dto;
using OnlineLibrary.Helper;
using OnlineLibrary.Model;

namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly OBDbcontext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        public BookController(OBDbcontext dbcontext , UserManager<ApplicationUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }
        [HttpGet("book")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<IActionResult> GetBookById(int id)
        {

            var book = await _dbcontext.Books.Where(x => x.Id == id).Include(c => c.Category)
                  .Select(x => new
                  {
                      x.Title,
                      x.Description,
                      x.Author,
                      category = x.Category.Name,
                        x.Price,
                        x.Stock,
                        x.PublishedYear,
                        BookReview= x.BookReviews.Select(x => new 
                        { 
                            x.Rating,
                            x.Comment,
                            x.DateTime
                        })
                    }).FirstOrDefaultAsync();
            return Ok(book);
        }
        [HttpPost("add-book")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddBook(AddBookDto addBookDto)
        {
            if (ModelState.IsValid)
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
                    return Ok(newBook);
                }
                else
                {
                    return BadRequest("the book is found");
                }
            }
            return BadRequest();
        }
        [HttpPut("edit_Book")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> EditBook(int id, [FromBody] EditBookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                var book = await _dbcontext.Books.FindAsync(id);
                if(book!=null)
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
                    return Ok(book);
                }
                return NotFound(" not found book");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Deletebook(int id)
        {
            if (ModelState.IsValid)
            {
                var book = await _dbcontext.Books.FindAsync(id);
                if (book != null)
                {
                    _dbcontext.Books.Remove(book);
                    await _dbcontext.SaveChangesAsync();
                    return Ok("the book removed Succesfully");

                }
                return NotFound("not found book");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("get")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> GetBook([FromQuery] PaginationParameters pagination)
        {
            var query = await _dbcontext.Books.AsQueryable().ToListAsync();
            var Count=await _dbcontext.Books.CountAsync();
            var books =  query.
                Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.GetValidParameter()).ToList();
            var result = new
            {
                TotalCount = Count,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (int)Math.Ceiling(Count / (double)pagination.PageSize),
                Data = books
            };
            return Ok(result);

        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchBook([FromQuery]SearchBookDto search)
        {
            if(ModelState.IsValid)
            {
                var books = await _dbcontext.Books.Where(x => x.Title.Contains(search.Title) || x.Author.Contains(search.Author) || x.Price == search.Price)
                    .Select(x => new
                    {
                        x.Title,
                        x.Description,
                        x.Author,
                        x.Price,
                        x.Stock,
                        x.PublishedYear,
                    }).ToListAsync();
                return Ok(books);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("BookRating")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> BookRating(BookRatingDto bookRating)
        {
            if (ModelState.IsValid)
            {
                var user=  await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var book = _dbcontext.Books.FindAsync(bookRating.BookId);
                    if(book==null)
                        return BadRequest("Not found Book");
                        var rating = new BookReview
                        {
                            BookId = bookRating.BookId,
                            Rating = bookRating.Rating,
                            Comment = bookRating.Comment,
                            DateTime = DateTime.UtcNow,
                            UserId=user.Id,
                        };
                        await _dbcontext.AddAsync(rating);
                        await _dbcontext.SaveChangesAsync();
                        return Ok(new
                        {
                            BookName = rating.book.Title,
                            Rating = rating.Rating,
                            Comment = rating.Comment,
                            DateTime = DateTime.UtcNow
                        });
                    }
                    return NotFound("not found user");
            }
            return BadRequest(ModelState);
        }
    }
}
