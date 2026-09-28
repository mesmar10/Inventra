using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;

namespace Inventra.Services.Interface
{
    public interface IAuthService
    {
        Task<Employee?> LoginAsync(LoginRequestDto request);
    }
}
