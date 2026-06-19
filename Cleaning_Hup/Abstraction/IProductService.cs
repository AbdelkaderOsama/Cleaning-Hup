using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace Cleaning_Hup.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(int id);
        Task<ProductResponse> CreateAsync(ProductRequest request);
        Task<ProductResponse> UpdateAsync(int id, ProductRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
