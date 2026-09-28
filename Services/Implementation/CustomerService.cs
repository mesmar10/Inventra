using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;

namespace Inventra.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> CreateCustomerAsync(
    CustomerRequestDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email
            };

            await _unitOfWork.Customer.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email
            };
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(id);

            if (customer == null)
                return false;

            _unitOfWork.Customer.DeleteAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers =
                await _unitOfWork.Customer.GetAllAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email
            });
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(id);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email
            };
        }

        public async Task<bool> UpdateCustomerAsync(int id,UpdateCustomerDto dto)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(id);

            if (customer == null)
                return false;

            customer.Name = dto.Name;
            customer.Phone = dto.Phone;
            customer.Email = dto.Email;

            _unitOfWork.Customer.UpdateAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}
