using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.InventoryDtos;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/inventories")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        // GET: api/dashboard/admin/inventories
        [HttpGet]
        public async Task<ICollection<Inventory>> GetInventories()
        {
            return await _inventoryRepository.GetInventoriesAsync();
        }

        // GET: api/dashboard/admin/inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _inventoryRepository.GetInventoryAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // POST: api/dashboard/admin/inventories
        [HttpPost]
        public async Task<ActionResult<Inventory>> CreateInventory(
            [FromForm] CreateInventoryDto inventory
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _inventoryRepository.IsInventoryItemExists(inventory.Name))
            {
                return BadRequest("Item already exists");
            }

            var createdInventory = await _inventoryRepository.CreateInventoryAsync(inventory);

            if (createdInventory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "Something went wrong" }
                );
            }

            return CreatedAtAction(
                nameof(GetInventory),
                new { id = createdInventory.Id },
                createdInventory
            );
        }

        // PUT: api/dashboard/admin/inventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(
            int id,
            [FromForm] UpdateInventoryDto inventory
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.Id)
            {
                return BadRequest("Id mismach");
            }

            if (!await _inventoryRepository.IsInventoryItemExists(id))
            {
                return NotFound();
            }

            if (await _inventoryRepository.IsInventoryItemExists(inventory.Name))
            {
                return BadRequest("Item already exists");
            }

            var updatedInventory = await _inventoryRepository.UpdateInventoryAsync(id, inventory);

            if (updatedInventory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "Something went wrong" }
                );
            }

            return Ok(inventory);
        }

        // DELETE: api/dashboard/admin/inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            if (!await _inventoryRepository.IsInventoryItemExists(id))
            {
                return NotFound();
            }

            if (!await _inventoryRepository.DeleteInventoryAsync(id))
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "Something went wrong" }
                );
            }

            return NoContent();
        }
    }
}
