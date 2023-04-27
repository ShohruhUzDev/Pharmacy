using System.Linq.Expressions;
using Pharmacy.Domain.Configurations;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Service.DTOs;

namespace Pharmacy.Service.Interfaces
{
    public interface ICustomerService
    {
        ValueTask<Customer> CreateAsync(CustomerForCreationDTO customerForCreationDTO);

        ValueTask<Customer> UpdateAsync(int id, CustomerForCreationDTO customerForCreationDTO);

        ValueTask<bool> DeleteAsync(Expression<Func<Customer, bool>> expression);

        ValueTask<IEnumerable<Customer>> GetAllAsync(
            PaginationParams @params = null,
            Expression<Func<Customer, bool>> expression = null);

        ValueTask<Customer> GetAsync(Expression<Func<Customer, bool>> expression);

    }
}
