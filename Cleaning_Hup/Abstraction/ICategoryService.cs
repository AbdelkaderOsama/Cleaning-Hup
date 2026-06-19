using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;

namespace Cleaning_Hup.Abstraction
{
    public interface ICategoryService
    {

        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> GetByIdAsync(int id);
        Task <CategoryResponse> CreateAsync(CategoryRequest  request);  
        Task<CategoryResponse> UpdateAsync(int id , CategoryRequest request);
        Task<bool> DeleteAsync (int id);
    }
}
