using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Domain.Entities.Users;
using Pharmacy.Entities.Medicines;
using Pharmacy.Entities.Orders;

namespace Pharmacy.Data.Contexts
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) :base (options)
        {}
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MedicineOrder> MedicineOrders { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicine>()
               .HasOne(med => med.Category)
               .WithMany(cat => cat.Medicines)
               .HasForeignKey(med => med.CategoryId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<MedicineOrder>()
              .HasOne(med => med.Basket)
              .WithMany(medO => medO.MedicineOrders)
              .HasForeignKey(med => med.BasketId)
              .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
