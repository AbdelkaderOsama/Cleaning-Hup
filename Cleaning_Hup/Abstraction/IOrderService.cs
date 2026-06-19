using System.Xml;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;

namespace Cleaning_Hup.Abstraction
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAllAsync();
        Task<OrderResponse> GetByIdAsync(int id);
        Task<OrderResponse> CreateAsync(OrderRequest request);
        Task<bool> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }
}
