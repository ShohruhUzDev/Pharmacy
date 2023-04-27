using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.IRepositories;
using Pharmacy.Domain.Configurations;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Exceptions;
using Pharmacy.Service.Extensions;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.Service.Services
{
    public class MedicineOrderService :IMedicineOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        public MedicineOrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async ValueTask<MedicineOrder> CreateAsync(MedicineOrderCreationDTO medicineOrderCreationDTO)
        {

            var MedicineOrder = await unitOfWork.MedicineOrders.CreateAsync(medicineOrderCreationDTO.Adapt<MedicineOrder>());
            await unitOfWork.SaveChangesAsync();

            return MedicineOrder;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<MedicineOrder, bool>> expression)
        {

            if (!(await unitOfWork.MedicineOrders.DeleteAsync(expression)))
                throw new PharmacyException(404, "MedicineOrder not found");

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<MedicineOrder>> GetAllAsync(PaginationParams @params = null,
            Expression<Func<MedicineOrder, bool>> expression = null)
        {
            var medicineOrders = unitOfWork.MedicineOrders.GetAll(expression,
                null, false);

            if (@params != null)
                return await medicineOrders.ToPagedList(@params).ToListAsync();

            return await medicineOrders.ToListAsync();

        }

        public async ValueTask<MedicineOrder> GetAsync(Expression<Func<MedicineOrder, bool>> expression)
        {
            return await unitOfWork.MedicineOrders.GetAsync(expression,
                null) ?? throw new PharmacyException(404, "MedicineOrder not found");
        }

        public async ValueTask<MedicineOrder> UpdateAsync(int id, MedicineOrderCreationDTO medicineOrderCreationDTO)
        {

            var medicineOrder = await GetAsync(c => c.Id == id);


            medicineOrder.UpdatedAt = DateTime.UtcNow;

            medicineOrder = unitOfWork.MedicineOrders.Update(medicineOrderCreationDTO.Adapt(medicineOrder));
            await unitOfWork.SaveChangesAsync();

            return medicineOrder;
        }
    }
}
