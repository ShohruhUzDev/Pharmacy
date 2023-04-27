using Pharmacy.Domain.Commons;

namespace Pharmacy.Entities.Medicines
{
    public sealed class Category : Auditable
    {
        public string Content { get; set; }
        public ICollection<Medicine> Medicines { get; set; }
    }
}
