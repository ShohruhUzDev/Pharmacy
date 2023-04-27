using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Domain.Entities.Users;
using Pharmacy.Entities.Medicines;
using Pharmacy.Entities.Orders;

namespace Pharmacy.Data.IRepositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Medicine> Medicines { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Basket> Baskets { get; }
        IGenericRepository<MedicineOrder> MedicineOrders { get; }
        IGenericRepository<Order> Orders { get; }
        ValueTask SaveChangesAsync();
    }
}
