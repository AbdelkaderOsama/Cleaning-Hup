using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Models;
using Cleaning_Hup.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Cleaning_Hup.Services.Classes
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryResponse>> GetAllAsync()
        {
            return await _context.Inventories
                .Include(i => i.Product)
                .Select(i => new InventoryResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    MinQuantity = i.MinQuantity,
                    IsLowStock = i.Quantity <= i.MinQuantity,
                    LastUpdated = i.LastUpdated
                }).ToListAsync();
        }

        public async Task<InventoryResponse?> GetByProductIdAsync(int productId)
        {
            var inventory = await _context.Inventories.Include(i => i.Product).FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) return null;
            return new InventoryResponse
            {
                Id = inventory.Id,
                ProductId = inventory.ProductId,
                ProductName = inventory.Product.Name,
                Quantity = inventory.Quantity,
                MinQuantity = inventory.MinQuantity,
                IsLowStock = inventory.Quantity <= inventory.MinQuantity,
                LastUpdated = inventory.LastUpdated
            };
        }

        public async Task UpdateQuantityAsync(int productId, decimal quantity, string type)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null)
            {
                inventory = new Inventory { ProductId = productId, Quantity = 0 };
                _context.Inventories.Add(inventory);
            }

            if (type == "IN")
                inventory.Quantity += quantity;
            else if (type == "OUT")
                inventory.Quantity -= quantity;

            inventory.LastUpdated = DateTime.UtcNow;

            _context.InventoryTransactions.Add(new InventoryTransaction
            {
                ProductId = productId,
                Quantity = quantity,
                TransactionType = type,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InventoryResponse>> GetLowStockAsync()
        {
            return await _context.Inventories
                .Include(i => i.Product)
                .Where(i => i.Quantity <= i.MinQuantity)
                .Select(i => new InventoryResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    MinQuantity = i.MinQuantity,
                    IsLowStock = true,
                    LastUpdated = i.LastUpdated
                }).ToListAsync();
        }
    }
}
