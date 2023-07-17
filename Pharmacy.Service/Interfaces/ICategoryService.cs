using System.ComponentModel;
using System.Linq.Expressions;
using Pharmacy.Domain.Configurations;
using Pharmacy.Entities.Medicines;
using Pharmacy.Service.DTOs;

namespace Pharmacy.Service.Interfaces
{
    public interface ICategoryService
    {
        ValueTask<Category> CreateAsync(CategoryForCreationDTO categoryForCreationDTO);

        ValueTask<Category> UpdateAsync(int id, CategoryForCreationDTO categoryForCreationDTO);

        ValueTask<bool> DeleteAsync(Expression<Func<Category, bool>> expression);

        ValueTask<IQueryable<Category>> GetAllAsync(
            PaginationParams @params = null,
            Expression<Func<Category, bool>> expression = null);

        ValueTask<Category> GetAsync(Expression<Func<Category, bool>> expression);
    }
}
