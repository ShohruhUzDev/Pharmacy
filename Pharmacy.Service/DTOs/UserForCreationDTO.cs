using System.ComponentModel.DataAnnotations;
using Pharmacy.Service.Attributes;

namespace Pharmacy.Service.DTOs
{
    public class UserForCreationDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [UserLogin, Required]
        public string Username { get; set; }

        [UserPassword, Required]
        public string Password { get; set; }

        [PhoneNumber, Required]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}