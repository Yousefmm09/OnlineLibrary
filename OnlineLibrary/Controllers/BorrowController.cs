using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Dto;
using OnlineLibrary.Helper;
using OnlineLibrary.Model;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly OBDbcontext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public BorrowController(OBDbcontext dbcontext, UserManager<ApplicationUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }
        [HttpGet("getallBorrow")]
        [Authorize(Roles = "User")]
        public IActionResult Get()
        {
            var borrows = _dbcontext.Borrows
                .Select(x => new
                {
                    x.Id,
                    x.Book.Title,
                    x.Customer.Name,
                    x.BorrowDate,
                    x.ReturnDate,
                    x.Status,
                    
                });
            return Ok(borrows);

        }
        [HttpPost("addBorrow")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Borrow([FromBody] BorrowDto dto)
        {
            var user= await  _userManager.GetUserAsync(User);
            var customer = _dbcontext.Customers.FirstOrDefault(c => c.UserId == user.Id);
            if (customer == null)
                return NotFound("customer not found");
            var book = _dbcontext.Books.FirstOrDefault(b=>b.Id==dto.BookId);
            if (book == null)
                return NotFound("book  not found");
            if (book.Stock < 0)
                return NotFound("this is book currently out of stock");
            var borrow = new Borrow
            {
                CustomerId = customer.Id,
                BookId = dto.BookId,
                BorrowDate = DateTime.UtcNow,
                ReturnDate = DateTime.UtcNow.AddDays(14),
                Status = "Borrowed"
            };
            book.Stock -=1;
            _dbcontext.Add(borrow);
            _dbcontext.SaveChanges();
            return Ok(new 
            {
                Message = "Book borrowed successfully",
                BorrowId = borrow.Id,
                DueDate = borrow.ReturnDate
            });
        }
        [HttpGet("returnborrow")]
        public IActionResult ReturnBorrow(int id)
        {
            var borrow=_dbcontext.Borrows.Include(c=>c.Book).FirstOrDefault(c=>c.Id==id);
            if (borrow == null) return NotFound("the borrow is not found");

            if (borrow.Status == "Returned")
                return BadRequest("Book already returned");
            borrow.Status = "Returned";
            borrow.ReturnDate = DateTime.UtcNow;
            borrow.Book.Stock +=1;
            _dbcontext.Borrows.Remove(borrow);
            _dbcontext.SaveChanges();
            return Ok(new
            {
                Message = "Book returned successfully",
            });

        }
        [HttpGet("PaginationBorrow")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> GetBorrowwithPagination([FromQuery]PaginationParameters pagination)
        {
            var borrow =  _dbcontext.Borrows.Select(x => new
            {
                x.Id,
                x.BorrowDate,
                x.Book.Title,
                x.ReturnDate,
            }).AsQueryable();
            var borrowcount=   borrow.Count();
            var paginatioborrow= borrow
                .Skip((pagination.PageNumber-1)*pagination.PageSize) // 1-1
                .Take(pagination.GetValidParameter());
            var result = new
            {
                TotalCount = borrowcount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (int)Math.Ceiling(borrowcount / (double)pagination.PageSize), // borrowcount =55  ,size=10  totalpages=6
                Data = borrow
            };
            
            return Ok(result);
        }
        
    }
}
