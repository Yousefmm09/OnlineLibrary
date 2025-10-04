using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineLibrary.Data;
using OnlineLibrary.Dto;
using OnlineLibrary.Model;
using OnlineLibrary.Repository;
using System.Diagnostics;

namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly OBDbcontext _dbcontext;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMemoryCache _cache;
        public CategoryController(OBDbcontext dbcontext,IMemoryCache cache , ICategoryRepo categoryRepo)
        {
            _dbcontext = dbcontext;
            _cache = cache;
            _categoryRepo = categoryRepo;
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]AddCategoryDto addCategory)
        {
            if (ModelState.IsValid)
            {
              await  _categoryRepo.AddCategoryAsync(addCategory);
                return Ok("the category is added successfully");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("getAllBookinCategory/{id}")]
        public async Task<IActionResult> GetAllBookinCategory(int id)
        {
            if(ModelState.IsValid)
            {
                var category =  await _dbcontext.Categories.FindAsync(id);
                if(category!=null)
                {
                    await _categoryRepo.GetAllBookinCategory(id);
                }
            }
            return BadRequest();
        }
        [HttpDelete("delete-category")]
        public  async Task<IActionResult> DeleteCategory(int id)
        {
            if(ModelState.IsValid)
            {
                   await _categoryRepo.DeleteCategoryAsync(id);
                    return Ok("the category is deleted");
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            if (!_cache.TryGetValue("Category", out object state))
            {
                var categories = await _categoryRepo.GetAllCategoriesAsync();
                state = new
                {
                    Category = categories
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
