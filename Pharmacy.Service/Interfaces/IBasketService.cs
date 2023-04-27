using System.Linq.Expressions;
using Pharmacy.Domain.Configurations;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Service.DTOs;

namespace Pharmacy.Service.Interfaces
{
    public interface IBasketService
    {
        ValueTask<Basket> CreateAsync(BasketForCreationDTO basketForCreationDTO);

        ValueTask<Basket> UpdateAsync(int id, BasketForCreationDTO basketForCreationDTO);

        ValueTask<bool> DeleteAsync(Expression<Func<Basket, bool>> expression);

        ValueTask<IEnumerable<Basket>> GetAllAsync(
            PaginationParams @params = null,
            Expression<Func<Basket, bool>> expression = null);

        ValueTask<Basket> GetAsync(Expression<Func<Basket, bool>> expression);

    }
}
