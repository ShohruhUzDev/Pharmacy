using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Entities.Medicines;

namespace Pharmacy.Service.DTOs
{
    public class MedicineOrderForCreationDTO
    {
        public int Count { get; set; }
        public int MedicineId { get; set; }
        public int BasketId { get; set; }
    }
}
