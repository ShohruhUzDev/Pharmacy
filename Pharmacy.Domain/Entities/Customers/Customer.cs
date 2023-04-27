using Pharmacy.Domain.Commons;

namespace Pharmacy.Domain.Entities.Customers
{
    public sealed class Customer : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
