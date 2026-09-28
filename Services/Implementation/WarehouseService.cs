using Microsoft.EntityFrameworkCore;
using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Mapping;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;

namespace Inventra.Services.Implementation
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IGenericRepository<Warehouse> warehouseRepository;
        public WarehouseService(IGenericRepository<Warehouse> warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }
        public async Task<WarehouseDto> CreateWarehouseAsync(WarehouseRequestDto warehouse)
        {
            var entity = warehouse.ToWarehouseDomain();
            await warehouseRepository.AddAsync(entity);
            await warehouseRepository.SaveAsync();

            // جلب الكائن مجدداً مع الـ Manager لإظهار اسمه بالاستجابة
            var createdWarehouse = await warehouseRepository.GetByIdAsync(entity.Id, w => w.Manager);
            return createdWarehouse!.ToWarehouseDto();
        }

        public async Task<bool> DeleteWarehouseAsync(int Id)
        {
            var existingWarehouse = await warehouseRepository.GetByIdAsync(Id);
            if (existingWarehouse == null)
                return false;
            warehouseRepository.DeleteAsync(existingWarehouse);
            await warehouseRepository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync()
        {
            var warehouses = await warehouseRepository.GetAllAsync(w => w.Manager);
            return warehouses.Select(w => w.ToWarehouseDto());
        }


        public async Task<WarehouseDetailsDto?> GetWarehouseByIdAsync(int id)
        {
            // أضف w => w.Employees لتضمين الموظفين التابعين للمستودع
            var warehouse = await warehouseRepository.GetByIdAsync(id, w => w.Manager, w => w.Employees);
            return warehouse?.ToWarehouseDetailsDto();
        }


        public async Task<bool> UpdateWarehouseAsync(int id, WarehouseUpdateDto warehouseDto)
        {
            var existingWarehouse = await warehouseRepository.GetByIdAsync(id);
            if (existingWarehouse == null)
                return false;

            existingWarehouse.Name = warehouseDto.Name;
            existingWarehouse.Location = warehouseDto.Location;
            existingWarehouse.ManagerId = warehouseDto.ManagerId;

           

            await warehouseRepository.SaveAsync();
            return true;
        }



    }
}

