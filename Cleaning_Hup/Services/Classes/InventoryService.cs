using AutoMapper;
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
        private readonly IMapper _mapper;

        public InventoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryResponse>> GetAllAsync()
        {
            var inventories = await _context.Inventories.Include(i => i.Product).ToListAsync();
            return _mapper.Map<IEnumerable<InventoryResponse>>(inventories);
        }

        public async Task<InventoryResponse?> GetByProductIdAsync(int productId)
        {
            var inventory = await _context.Inventories.Include(i => i.Product).FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) return null;
            return _mapper.Map<InventoryResponse>(inventory);
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
            var inventories = await _context.Inventories
                .Include(i => i.Product)
                .Where(i => i.Quantity <= i.MinQuantity)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InventoryResponse>>(inventories);
        }
    }
}
