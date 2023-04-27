using System.Linq.Expressions;
using Pharmacy.Domain.Configurations;
using Pharmacy.Entities.Orders;
using Pharmacy.Service.DTOs;

namespace Pharmacy.Service.Interfaces
{
    public interface IOrderService
    {
        ValueTask<Order> CreateAsync(OrdersForCreationDTO orderForCreationDTO);

        ValueTask<Order> UpdateAsync(int id, OrdersForCreationDTO orderForCreationDTO);

        ValueTask<bool> DeleteAsync(Expression<Func<Order, bool>> expression);

        ValueTask<IEnumerable<Order>> GetAllAsync(
            PaginationParams @params = null,
            Expression<Func<Order, bool>> expression = null);

        ValueTask<Order> GetAsync(Expression<Func<Order, bool>> expression);
    }
}