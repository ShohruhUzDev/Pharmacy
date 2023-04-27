using Pharmacy.Domain.Commons;
using Pharmacy.Entities.Medicines;

namespace Pharmacy.Domain.Entities.Orders
{
    public sealed class MedicineOrder : Auditable
    {
        public int Count { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
