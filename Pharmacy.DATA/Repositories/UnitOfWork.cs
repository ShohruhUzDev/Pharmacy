using Pharmacy.Data.Contexts;
using Pharmacy.Data.IRepositories;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Domain.Entities.Users;
using Pharmacy.Entities.Medicines;
using Pharmacy.Entities.Orders;

namespace Pharmacy.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PharmacyDbContext dbContext;

        public IGenericRepository<Medicine> Medicines { get; }
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Basket> Baskets { get; }
        public IGenericRepository<MedicineOrder> MedicineOrders { get; }

        public UnitOfWork(PharmacyDbContext dbContext)
        {
            this.dbContext = dbContext;
            Medicines = new GenericRepository<Medicine>(dbContext);
            Users = new GenericRepository<User>(dbContext);
            Orders = new GenericRepository<Order>(dbContext);
            Customers= new GenericRepository<Customer>(dbContext);
            Categories=new GenericRepository<Category>(dbContext);
            Baskets=new GenericRepository<Basket>(dbContext);
            MedicineOrders=new GenericRepository<MedicineOrder>(dbContext); 
        }



        public async ValueTask SaveChangesAsync() =>
            await dbContext.SaveChangesAsync();
    }
}
