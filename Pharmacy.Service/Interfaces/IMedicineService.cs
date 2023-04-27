using System.Linq.Expressions;
using Pharmacy.Domain.Configurations;
using Pharmacy.Entities.Medicines;
using Pharmacy.Service.DTOs;

namespace Pharmacy.Service.Interfaces
{
    public interface IMedicineService
    {
        ValueTask<Medicine> CreateAsync(MedicineForCreationDTO medicineForCreationDTO);

        ValueTask<Medicine> UpdateAsync(int id, MedicineForCreationDTO medicineForCreationDTO);

        ValueTask<bool> DeleteAsync(Expression<Func<Medicine, bool>> expression);

        ValueTask<IEnumerable<Medicine>> GetAllAsync(
            PaginationParams @params = null,
            Expression<Func<Medicine, bool>> expression = null);

        ValueTask<Medicine> GetAsync(Expression<Func<Medicine, bool>> expression);

    }
}
