namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="ADMIN")]
    public class AdminController : ControllerBase
    {
        private readonly OBDbcontext _dbcontext;
        private readonly IMemoryCache _cache;
        public AdminController(OBDbcontext dbcontext, IMemoryCache cache)
        {
            _dbcontext = dbcontext;
            _cache = cache;
        }
        [HttpGet("getallusers")]
        public IActionResult Get()
        {
            var customers = _dbcontext.Customers
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.EmailAddress,
                    c.PhoneNumber,
                    c.Adress
                });
            return Ok(customers);
        }
        [HttpDelete("deleteuser/{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _dbcontext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound("User not found");
            }
            _dbcontext.Customers.Remove(customer);
            _dbcontext.SaveChanges();
            return Ok("User deleted successfully");
        }
        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            if (!_cache.TryGetValue("Dashboard", out object state))
            {
                var mostBorrowedBooks = await _dbcontext.Borrows
                    .GroupBy(b => b.BookId)
                    .Select(g => new
                    {
                        BookId = g.Key,
                        BorrowCount = g.Count()
                    })
                    .OrderByDescending(g => g.BorrowCount)
                    .Take(5)
                    .Join(_dbcontext.Books,
                        borrowGroup => borrowGroup.BookId,
                        book => book.Id,
                        (bg, b) => new
                        {
                            b.Id,
                            b.Title,
                            bg.BorrowCount
                        })
                    .ToListAsync();

                var customer = await _dbcontext.Customers.CountAsync();
                var books = await _dbcontext.Books.CountAsync();
                var borrow = await _dbcontext.Borrows.CountAsync();
                var order = await _dbcontext.Orders.CountAsync();

                var salesOfMonth = await _dbcontext.Orders
                    .Where(o => o.OrderDate.Month == DateTime.UtcNow.Month)
                    .Select(x => new
                    {
                        Order = x.Id,
                        OrderItem = x.OrderDate,
                        Books = x.Items.Select(b => new
                        {
                            b.Book.Title,
                            b.Quantity,
                            b.Price
                        }),
                        TotalOrderAmount = x.Items.Sum(i => i.Price * i.Quantity)
                    })
                    .ToListAsync();

                state = new
                {
                    Customer = customer,
                    Book = books,
                    Order = order,
                    Borrow = borrow,
                    BorrowBook = mostBorrowedBooks,
                    SalesOfMonth = salesOfMonth
                };

                _cache.Set("Dashboard", state, TimeSpan.FromMinutes(5));
            }

            return Ok(state);
        }
        [HttpGet("Stock")]
        public async Task<IActionResult> StockBooks()
        {
            if(ModelState.IsValid)
            {
                if (!_cache.TryGetValue("Stock", out object state))
                {
                    var book = await _dbcontext.Books
                    .OrderByDescending(x => x.Stock)
                    .Select(x => new
                    {
                        x.Id,
                        x.Title,
                        x.Stock,
                    }).ToListAsync();
                    state = new
                    {
                        books = book,
                    };
                    _cache.Set("Stock",state, TimeSpan.FromMinutes(5));
                }
                return Ok(state);
            }
            return BadRequest(ModelState);
        }

    }
}
            
