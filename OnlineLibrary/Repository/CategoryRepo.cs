using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineLibrary.Data;
using OnlineLibrary.Dto;
using OnlineLibrary.Model;

namespace OnlineLibrary.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly OBDbcontext _dbcontext;
        private readonly IMemoryCache _cache;
        public CategoryRepo(OBDbcontext dbcontext, IMemoryCache memory)
        {
            _dbcontext = dbcontext;
            _cache = memory;
        }
        public async Task AddCategoryAsync(AddCategoryDto categorydto)
        {
            var name = await _dbcontext.Categories.FirstOrDefaultAsync(x => x.Name == categorydto.Name);
            if (name == null)
            {
                var category = new Category
                {
                    Name = categorydto.Name,
                    Description = categorydto.Description,
                };
                _dbcontext.Categories.Add(category);
                await _dbcontext.SaveChangesAsync();

            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbcontext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbcontext.Remove(category);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<List<AddCategoryDto>?> GetAllCategoriesAsync()
        {
            var category = await _dbcontext.Categories
                .Select(x => new AddCategoryDto
                {
                    Name = x.Name,
                    Description = x.Description,
                }).ToListAsync();
            return category;
        }


        public async Task<List<GetAllBookinCategoryDto>?> GetAllBookinCategory(int id)
        {
            var book = await _dbcontext.Books.Where(x => x.CategoryId == id)
                       .Select(x => new GetAllBookinCategoryDto()
                       {
                           Title = x.Title,
                           Author = x.Author,
                           Price = x.Price,
                           ISBN = x.ISBN,
                           Stock = x.Stock,
                           Description = x.Description,
                       }).ToListAsync();

            return book;
            
        }
    }
}
