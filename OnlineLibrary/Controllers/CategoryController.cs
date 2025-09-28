using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineLibrary.Data;
using OnlineLibrary.Dto;
using OnlineLibrary.Model;
using System.Diagnostics;

namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly OBDbcontext _dbcontext;
        private readonly IMemoryCache _cache;
        public CategoryController(OBDbcontext dbcontext,IMemoryCache cache)
        {
            _dbcontext = dbcontext;
            _cache = cache;
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]AddCategoryDto addCategory)
        {
            var name = _dbcontext.Categories.FirstOrDefault(x => x.Name==addCategory.Name);
            if (name ==null)
            {
                var category = new Category

                {
                    Name= addCategory.Name,
                };
                await  _dbcontext.Categories.AddAsync(category);
                await  _dbcontext.SaveChangesAsync();
            }
            else { return BadRequest("the category is found"); }
            return Ok("the book is added");
        }
        [HttpGet("getAllBookinCategory/{id}")]
        public async Task<IActionResult> GetAllBookinCategory(int id)
        {
            if(ModelState.IsValid)
            {
                var category =  await _dbcontext.Categories.FindAsync(id);
                if(category!=null)
                {
                    var book = await _dbcontext.Books.Where(x => x.CategoryId == id)
                        .Select(x => new GetAllBookinCategoryDto()
                        {
                            Title = x.Title,
                            Author=x.Author,
                            Price=x.Price,
                            ISBN=x.ISBN,
                            Stock=x.Stock,
                            Description=x.Description,
                        }).ToListAsync();
                    return Ok(book);
                }
            }
            return BadRequest();
        }
        [HttpDelete("delete-category")]
        public IActionResult DeleteCategory(int id)
        {
            if(ModelState.IsValid)
            {
                var category= _dbcontext.Categories.Find(id);
                if(category!=null)
                {
                    _dbcontext.Categories.Remove(category);
                    _dbcontext.SaveChanges();
                    return Ok("the category is deleted");
                }
                return BadRequest("the category is not found");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            if (!_cache.TryGetValue("Category", out object state))
            {
                //var category = await _dbcontext.Categories.Select(x => new
                //{
                //    x.Name
                //}).ToListAsync();
                var car = await _dbcontext.Categories
                .Select(x => new
                {
                    x.Name
                }).ToListAsync();
                state = new
                {
                    Category = car
                };
                _cache.Set("Category", state, TimeSpan.FromHours(1));
            }
            return Ok(new
            {
                Data = state,
            });
        }
    }
}
