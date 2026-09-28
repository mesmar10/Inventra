using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;

namespace Inventra.Mapping
{
    public static class UnitMapping
    {
        public static Unit ToUnitDomin(this UnitRequestDto dto)
        {
            return new Unit
            {
                Name = dto.Name
            };
        }
        public static UnitDto ToUnitDto(this Unit unit)
        {
            return new UnitDto
            {
                Id = unit.Id,
                Name = unit.Name
            };
        }
    }
}
