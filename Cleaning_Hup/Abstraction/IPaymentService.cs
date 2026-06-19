using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;

namespace Cleaning_Hup.Abstraction
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentResponse>> GetByOrderIdAsync(int orderId);
        Task<PaymentResponse> CreateAsync(PaymentRequest request);
    }
}
