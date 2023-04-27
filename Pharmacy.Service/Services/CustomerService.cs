using Mapster;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.IRepositories;
using Pharmacy.Domain.Configurations;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Exceptions;
using Pharmacy.Service.Extensions;
using System.Linq.Expressions;

namespace Pharmacy.Service.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async ValueTask<Customer> CreateAsync(CustomerForCreationDTO customerForCreationDTO)
        {

            var Customer = await unitOfWork.Customers.CreateAsync(customerForCreationDTO.Adapt<Customer>());
            await unitOfWork.SaveChangesAsync();

            return Customer;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Customer, bool>> expression)
        {

            if (!(await unitOfWork.Customers.DeleteAsync(expression)))
                throw new PharmacyException(404, "Customer not found");

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<Customer>> GetAllAsync(PaginationParams @params = null,
            Expression<Func<Customer, bool>> expression = null)
        {
            var customers = unitOfWork.Customers.GetAll(expression, new string[] { "Attachment" }, false);

            if (@params != null)
                return await customers.ToPagedList(@params).ToListAsync();

            return await customers.ToListAsync();

        }

        public async ValueTask<Customer> GetAsync(Expression<Func<Customer, bool>> expression)
        {
            return await unitOfWork.Customers.GetAsync(expression,
                    new string[] { "Attachment" }) ?? throw new PharmacyException(404, "Customer not found");
        }

        public async ValueTask<Customer> UpdateAsync(int id, CustomerForCreationDTO customerForCreationDTO)
        {

            var customer = await GetAsync(c => c.Id == id);


            customer.UpdatedAt = DateTime.UtcNow;

            customer = unitOfWork.Customers.Update(customerForCreationDTO.Adapt(customer));
            await unitOfWork.SaveChangesAsync();

            return customer;
        }
    }
}
