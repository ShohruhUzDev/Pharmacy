using Pharmacy.Domain.Commons;
using Pharmacy.Domain.Entities.Users;

namespace Pharmacy.Domain.Entities.Orders
{
    public sealed class Basket : Auditable
    {
        public int TotalPrice { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<MedicineOrder> MedicineOrders { get; set; }
    }
}
