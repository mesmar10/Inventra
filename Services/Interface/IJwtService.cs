using Inventra.Models.DomainModels;

namespace Inventra.Services.Interface
{
    public interface IJwtService
    {
        string GenerateToken(Employee employee);
    }
}
