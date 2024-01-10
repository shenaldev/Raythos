using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Categories;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryDto>> GetCategories()
        {
            return _mapper.Map<ICollection<CategoryDto>>(await _context.Categories.ToListAsync());
        }

        public async Task<CategoryDto?> GetCategory(int id)
        {
            return _mapper.Map<CategoryDto>(await _context.Categories.FindAsync(id));
        }

        public async Task<CategoryDto?> CreateCategory(CreateCategoryDto category)
        {
            try
            {
                Category newCategory = _mapper.Map<Category>(category);
                newCategory.Slug = newCategory.Name.ToLower().Replace(" ", "-");
                newCategory.CreatedAt = DateTime.Now;
                newCategory.UpdatedAt = DateTime.Now;

                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return _mapper.Map<CategoryDto>(newCategory);
            }
            catch
            {
                return null;
            }
        }

        public async Task<CategoryDto?> UpdateCategory(UpdateCategoryDto category)
        {
            try
            {
                Category? existingCategory = await _context.Categories.FindAsync(category.Id);
                if (existingCategory == null)
                {
                    return null;
                }

                existingCategory.Name = category.Name;
                existingCategory.Slug = category.Name.ToLower().Replace(" ", "-");
                existingCategory.UpdatedAt = DateTime.Now;

                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
                return _mapper.Map<CategoryDto>(existingCategory);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                Category? existingCategory = await _context.Categories.FindAsync(id);
                if (existingCategory == null)
                {
                    return false;
                }

                _context.Categories.Remove(existingCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsCategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> IsCategoryExists(string slug)
        {
            return await _context.Categories.AnyAsync(c => c.Slug == slug);
        }
    }
}
