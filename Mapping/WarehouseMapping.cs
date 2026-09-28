using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Models.DomainModels;

namespace Inventra.Mapping
{
    public static class WarehouseMapping
    {
        public static Warehouse ToWarehouseDomain(this WarehouseRequestDto dto)
        {
            return new Warehouse
            {
                Name = dto.Name,
                Location = dto.Location,
                ManagerId = dto.ManagerId
            };
        }
        public static WarehouseDto ToWarehouseDto(this Warehouse warehouse)
        {
            return new WarehouseDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Location = warehouse.Location,
                ManagerId = warehouse.ManagerId,
                ManagerName = warehouse.Manager?.Name ?? string.Empty
            };
        }
        public static Warehouse ToWarehouseDomain(this WarehouseUpdateDto dto)
        {
            return new Warehouse
            {
                Name = dto.Name,
                Location = dto.Location,
                ManagerId = dto.ManagerId
            };
        }
        public static WarehouseDetailsDto ToWarehouseDetailsDto(this Warehouse warehouse)
        {
            return new WarehouseDetailsDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Location = warehouse.Location,
                ManagerId = warehouse.ManagerId,
                ManagerName = warehouse.Manager?.Name ?? string.Empty,
                Employees = warehouse.Employees?.Select(e=>e.ToEmployeeDto()).ToList()?? new List<EmployeeDto>(),
                ProductWarehouses = warehouse.ProductWarehouses?.Select(p=>p.ToDto()).ToList()?? new List<ProductWarehouseDto>()
            };
        } 
    }
}
