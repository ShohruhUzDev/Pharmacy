using Pharmacy.Domain.Commons;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Orders;

namespace Pharmacy.Entities.Orders
{
    public sealed class Order : Auditable
    {
        public string Location { get; set; }
        public bool IsPayed { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
