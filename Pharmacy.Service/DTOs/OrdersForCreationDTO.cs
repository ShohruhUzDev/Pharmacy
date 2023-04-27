using System.Text.Json.Serialization;
using Pharmacy.Domain.Entities.Customers;
using Pharmacy.Domain.Entities.Orders;

namespace Pharmacy.Service.DTOs
{
    public class OrdersForCreationDTO
    {

        public string Location { get; set; }
        public bool IsPayed { get; set; }
        public int BasketId { get; set; }
        public int CustomerId { get; set; }
    }
}
