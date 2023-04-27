using Pharmacy.Domain.Commons;
using Pharmacy.Domain.Enums;

namespace Pharmacy.Domain.Entities.Users
{
    public sealed class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
    }
}
