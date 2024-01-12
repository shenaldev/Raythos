using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.InventoryDtos;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InventoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Inventory>> GetInventoriesAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory?> GetInventoryAsync(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }

        public async Task<Inventory?> CreateInventoryAsync(CreateInventoryDto inventory)
        {
            try
            {
                Inventory newItem = _mapper.Map<Inventory>(inventory);
                newItem.CreatedAt = DateTime.Now;
                newItem.UpdatedAt = DateTime.Now;
                await _context.Inventories.AddAsync(newItem);
                await _context.SaveChangesAsync();
                return newItem;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Inventory?> UpdateInventoryAsync(int id, UpdateInventoryDto inventory)
        {
            try
            {
                Inventory updateItem = _mapper.Map<Inventory>(inventory);
                updateItem.UpdatedAt = DateTime.Now;

                _context.Entry(updateItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return updateItem;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteInventoryAsync(int id)
        {
            try
            {
                Inventory? inventory = await _context.Inventories.FindAsync(id);
                if (inventory == null)
                {
                    return false;
                }
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> IsInventoryItemExists(int id)
        {
            return _context.Inventories.AnyAsync(i => i.Id == id);
        }

        public Task<bool> IsInventoryItemExists(string name)
        {
            return _context.Inventories.AnyAsync(i => i.Name == name);
        }
    }
}
