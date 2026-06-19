using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;

namespace Cleaning_Hup.Abstraction
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryResponse>> GetAllAsync();
        Task<InventoryResponse?> GetByProductIdAsync(int productId);
        Task UpdateQuantityAsync(int productId, decimal quantity, string type); // IN / OUT
        Task<IEnumerable<InventoryResponse>> GetLowStockAsync();
    }
}
