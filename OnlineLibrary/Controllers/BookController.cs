using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Dto;
using OnlineLibrary.Helper;
using OnlineLibrary.Model;
using OnlineLibrary.Repository;
using System.Reflection.Metadata.Ecma335;

namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookRepositroy _bookRepository;
        public BookController( UserManager<ApplicationUser> userManager , IBookRepositroy repositroy)
        {
            _userManager = userManager;
            _bookRepository = repositroy;
        }
        [HttpGet("book")]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<IActionResult> GetBookById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepository.GetBookById(id);
            if(book!=null) 
               return Ok(book);
            return NotFound("the book is not found ");
        }
        [HttpPost("add-book")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddBook(AddBookDto addBookDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    await _bookRepository.AddBookAsync(addBookDto);
                    return Ok("the book added Succesfully");
                }
                return NotFound("not found user");
            }
            return BadRequest();
        }
        [HttpPut("edit_Book")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> EditBook(int id, [FromBody] EditBookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                var book =  _bookRepository.EditBook(id, bookDto);
                return Ok(book);
                
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Deletebook(int id)
        {
            if (ModelState.IsValid)
            {
                 await  _bookRepository.DeleteBookAsync(id);
                return Ok("the book is removed");
            }
            return BadRequest(ModelState);
        }
        //[HttpGet("get")]
        //[Authorize(Roles = "USER")]
        //public async Task<IActionResult> GetBook([FromQuery] PaginationParameters pagination)
        //{
        //    var query = await _dbcontext.Books.AsQueryable().ToListAsync();
        //    var Count=await _dbcontext.Books.CountAsync();
        //    var books =  query.
        //        Skip((pagination.PageNumber - 1) * pagination.PageSize)
        //        .Take(pagination.GetValidParameter()).ToList();
        //    var result = new
        //    {
        //        TotalCount = Count,
        //        PageNumber = pagination.PageNumber,
        //        PageSize = pagination.PageSize,
        //        TotalPages = (int)Math.Ceiling(Count / (double)pagination.PageSize),
        //        Data = books
        //    };
        //    return Ok(result);

        //}
        //[HttpGet("search")]
        //public async Task<IActionResult> SearchBook([FromQuery]SearchBookDto search)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var books = await _dbcontext.Books.Where(x => x.Title.Contains(search.Title) || x.Author.Contains(search.Author) || x.Price == search.Price)
        //            .Select(x => new
        //            {
        //                x.Title,
        //                x.Description,
        //                x.Author,
        //                x.Price,
        //                x.Stock,
        //                x.PublishedYear,
        //            }).ToListAsync();
        //        return Ok(books);
        //    }
        //    return BadRequest(ModelState);
        //}
        //[HttpPost("BookRating")]
        //[Authorize(Roles ="USER")]
        //public async Task<IActionResult> BookRating(BookRatingDto bookRating)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user=  await _userManager.GetUserAsync(User);
        //        if (user != null)
        //        {
        //            var book = _dbcontext.Books.FindAsync(bookRating.BookId);
        //            if(book==null)
        //                return BadRequest("Not found Book");
        //                var rating = new BookReview
        //                {
        //                    BookId = bookRating.BookId,
        //                    Rating = bookRating.Rating,
        //                    Comment = bookRating.Comment,
        //                    DateTime = DateTime.UtcNow,
        //                    UserId=user.Id,
        //                };
        //                await _dbcontext.AddAsync(rating);
        //                await _dbcontext.SaveChangesAsync();
        //                return Ok(new
        //                {
        //                    BookName = rating.book.Title,
        //                    Rating = rating.Rating,
        //                    Comment = rating.Comment,
        //                    DateTime = DateTime.UtcNow
        //                });
        //            }
        //            return NotFound("not found user");
        //    }
        //    return BadRequest(ModelState);
        //}
    }
}
