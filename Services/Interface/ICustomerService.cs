using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;

namespace Inventra.Services.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();

        Task<CustomerDto?> GetCustomerByIdAsync(int id);

        Task<CustomerDto> CreateCustomerAsync(CustomerRequestDto dto);

        Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDto dto);

        Task<bool> DeleteCustomerAsync(int id);
    }
}
