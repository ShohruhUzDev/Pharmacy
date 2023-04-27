using System.Linq.Expressions;
using Pharmacy.Domain.Configurations;
using Pharmacy.Domain.Entities.Users;
using Pharmacy.Domain.Enums;
using Pharmacy.Service.Attributes;
using Pharmacy.Service.DTOs;

namespace Pharmacy.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO);

        ValueTask<UserForViewDTO> UpdateAsync(string login, string password, UserForUpdateDTO userForUpdateDTO);

        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);

        ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(
            PaginationParams @params,
            Expression<Func<User, bool>> expression = null);

        ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> expression);

        ValueTask<bool> ChangeRoleAsync(int id, UserRole userRole);

        ValueTask<bool> ChangePasswordAsync(string oldPassword, [UserPassword] string newPassword);
    }
}
