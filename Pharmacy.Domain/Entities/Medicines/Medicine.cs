using Pharmacy.Domain.Commons;

namespace Pharmacy.Entities.Medicines
{
    public sealed class Medicine : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int ExperationDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
