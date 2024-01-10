using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Categories;
using Raythos.Interfaces;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/categories")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/dashboard/admin/categories
        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategories()
        {
            return Ok(await _categoryRepository.GetCategories());
        }

        // GET: api/dashboard/admin/categories/5
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

        // POST: api/dashboard/admin/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(
            [FromForm] CreateCategoryDto category
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string slug = category.Name.ToLower().Replace(" ", "-");
            if (await _categoryRepository.IsCategoryExists(slug))
            {
                ModelState.AddModelError("Name", "Category already exists");
                return BadRequest(ModelState);
            }

            CategoryDto? newCategory = await _categoryRepository.CreateCategory(category);
            if (newCategory == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, newCategory);
        }

        // PUT: api/dashboard/admin/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute] int id,
            [FromForm] UpdateCategoryDto category
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                ModelState.AddModelError("Id", "Id mismatch or null");
                return BadRequest(ModelState);
            }

            if (!await _categoryRepository.IsCategoryExists(id))
            {
                return NotFound();
            }

            string slug = category.Name.ToLower().Replace(" ", "-");
            if (await _categoryRepository.IsCategoryExists(slug))
            {
                ModelState.AddModelError("Name", "Category already exists");
                return BadRequest(ModelState);
            }

            var updatedCategory = await _categoryRepository.UpdateCategory(category);
            if (updatedCategory == null)
            {
                return BadRequest();
            }

            return Ok(updatedCategory);
        }

        // DELETE: api/dashboard/admin/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!await _categoryRepository.IsCategoryExists(id))
            {
                return NotFound();
            }

            if (!await _categoryRepository.DeleteCategory(id))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
