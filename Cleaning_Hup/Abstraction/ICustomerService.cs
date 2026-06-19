using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;

namespace Cleaning_Hup.Abstraction
{
    public interface ICustomerService
    {
    
        Task<IEnumerable<CustomerResponse>> GetAllAsync();
        Task<CustomerResponse> GetByIdAsync(int id);
        Task<CustomerResponse> CreateAsync(CustomerRequest request);
        Task<CustomerResponse> UpdateAsync(int id, CustomerRequest request);
        Task<bool> DeleteAsync (int id);

    
    }
}
