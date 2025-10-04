using OnlineLibrary.Dto;
using OnlineLibrary.Model;

namespace OnlineLibrary.Repository
{
    public interface ICategoryRepo
    {
        Task<List<AddCategoryDto>> GetAllCategoriesAsync();
        Task<List<GetAllBookinCategoryDto>?> GetAllBookinCategory(int id);
        Task AddCategoryAsync(AddCategoryDto category);
        Task DeleteCategoryAsync(int id);
    }
}
