using Raythos.DTOs.Categories;

namespace Raythos.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<CategoryDto>> GetCategories();

        Task<CategoryDto?> GetCategory(int id);

        Task<CategoryDto?> CreateCategory(CreateCategoryDto category);

        Task<CategoryDto?> UpdateCategory(UpdateCategoryDto category);

        Task<bool> DeleteCategory(int id);

        Task<bool> IsCategoryExists(int id);

        Task<bool> IsCategoryExists(string slug);
    }
}
