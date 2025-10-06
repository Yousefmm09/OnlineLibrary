namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableRateLimiting("FixedWindow")]
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
        //[Authorize(Roles = "USER,ADMIN")]
        [EnableRateLimiting("TokenBucket")]
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
        [HttpGet("get")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> GetBook([FromQuery] PaginationParameters pagination)
        {
            var result = await _bookRepository.GetPaginationParametersAsync(pagination);
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchBook([FromQuery] SearchBookDto search)
        {
            if (ModelState.IsValid)
            {
                 var res = await _bookRepository.SearchBooksAsync(search);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("BookRating")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> BookRating(BookRatingDto bookRating)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var res = await _bookRepository.GetRatingParametersAsync(user.Id,bookRating);

                    return Ok(res);
                }
                return NotFound("not found user");
            }
            return BadRequest(ModelState);
        }
    }
}
