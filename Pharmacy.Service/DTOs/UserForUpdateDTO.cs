using System.ComponentModel.DataAnnotations;
using Pharmacy.Service.Attributes;

namespace Pharmacy.Service.DTOs
{
    public class UserForUpdateDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [UserLogin, Required]
        public string Username { get; set; }

        [PhoneNumber, Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
