using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Contracts.Response;

namespace Cleaning_Hup.Abstraction
{
    public interface IAuthService
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
    }
}
