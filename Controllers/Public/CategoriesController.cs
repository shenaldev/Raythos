using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Categories;
using Raythos.Interfaces;

namespace Raythos.Controllers.Public
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategories()
        {
            return Ok(await _categoryRepository.GetCategories());
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            CategoryDto? category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}
