using Raythos.DTOs.InventoryDtos;
using Raythos.Models;

namespace Raythos.Interfaces
{
    public interface IInventoryRepository
    {
        Task<ICollection<Inventory>> GetInventoriesAsync();

        Task<Inventory?> GetInventoryAsync(int id);

        Task<Inventory?> CreateInventoryAsync(CreateInventoryDto inventory);

        Task<Inventory?> UpdateInventoryAsync(int id, UpdateInventoryDto inventory);

        Task<bool> DeleteInventoryAsync(int id);

        Task<bool> IsInventoryItemExists(int id);

        Task<bool> IsInventoryItemExists(string name);
    }
}
