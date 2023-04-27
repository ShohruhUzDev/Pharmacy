using System.Text.Json.Serialization;

namespace Pharmacy.Service.DTOs
{
    public class OrdersForCreationDTO
    {
        public int UserId { get; set; }
        public int PharmacyId { get; set; }
    }
}
