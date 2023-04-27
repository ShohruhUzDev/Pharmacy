namespace Pharmacy.Service.DTOs
{
    public class MedicineForCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StoragePeriod { get; set; }
        public int CategoryId { get; set; }
    }
}
