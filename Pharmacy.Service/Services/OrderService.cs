using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.IRepositories;
using Pharmacy.Domain.Configurations;
using Pharmacy.Entities.Orders;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Exceptions;
using Pharmacy.Service.Extensions;
using Pharmacy.Service.Helpers;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask<Order> CreateAsync(OrdersForCreationDTO orderForCreationDTO)
        {
            orderForCreationDTO.UserId = HttpContextHelper.UserId ?? throw new PharmacyException(404, "User not found");

            var Medicine = await unitOfWork.Medicines.GetAsync(u => u.Id == orderForCreationDTO.PharmacyId);

            if (Medicine is null)
                throw new PharmacyException(404, "Pharmacy not found");

            var order = await unitOfWork.Orders.CreateAsync(orderForCreationDTO.Adapt<Order>());
            await unitOfWork.SaveChangesAsync();
            return order;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var isDeleted = await unitOfWork.Orders.DeleteAsync(expression);

            await unitOfWork.SaveChangesAsync();

            return isDeleted ? true : throw new PharmacyException(404, "Order not found");
        }

        public async ValueTask<IEnumerable<Order>> GetAllAsync(PaginationParams @params = null,
            Expression<Func<Order, bool>> expression = null)
        {
            var orders = unitOfWork.Orders.GetAll(expression: expression, new string[] { "User", "Pharmacy" }, false);

            return await orders.ToPagedList(@params).ToListAsync();
        }

        public async ValueTask<Order> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var order = await unitOfWork.Orders.GetAsync(expression, new string[] { "User", "Pharmacy" });
            return order ?? throw new PharmacyException(404, "Order not foud");
        }

        public async ValueTask<Order> UpdateAsync(int id, OrdersForCreationDTO orderForCreationDTO)
        {
            var order = await GetAsync(o => o.Id == id);

            orderForCreationDTO.UserId = HttpContextHelper.UserId ?? throw new PharmacyException(404, "User not found");

            var Medicine = await unitOfWork.Medicines.GetAsync(u => u.Id == orderForCreationDTO.PharmacyId);

            if (Medicine is null)
                throw new PharmacyException(404, "Medicine not found");

            order.UpdatedAt = DateTime.UtcNow;

            order = unitOfWork.Orders.Update(orderForCreationDTO.Adapt(order));


            await unitOfWork.SaveChangesAsync();

            return order;
        }
    }
}
