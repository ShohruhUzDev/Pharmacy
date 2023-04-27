using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Configurations;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Entities.Medicines;
using Pharmacy.Service.DTOs;

namespace Pharmacy.Service.Interfaces
{
    public interface IMedicineOrderService
    {
        ValueTask<MedicineOrder> CreateAsync(MedicineOrderForCreationDTO medicineOrderCreationDTO);

        ValueTask<MedicineOrder> UpdateAsync(int id, MedicineOrderForCreationDTO medicineOrderCreationDTO);

        ValueTask<bool> DeleteAsync(Expression<Func<MedicineOrder, bool>> expression);

        ValueTask<IEnumerable<MedicineOrder>> GetAllAsync(
            PaginationParams @params = null,
            Expression<Func<MedicineOrder, bool>> expression = null);

        ValueTask<MedicineOrder> GetAsync(Expression<Func<MedicineOrder, bool>> expression);

    }
}
