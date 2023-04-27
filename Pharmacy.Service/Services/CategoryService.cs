using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.IRepositories;
using Pharmacy.Domain.Configurations;
using Pharmacy.Entities.Medicines;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Exceptions;
using Pharmacy.Service.Extensions;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async ValueTask<Category> CreateAsync(CategoryForCreationDTO categoryForCreationDTO)
        {
            var alreadyExists = await unitOfWork.Categories.GetAsync(
                c => c.Content == categoryForCreationDTO.Content);

            if (alreadyExists != null)
                throw new PharmacyException(400, "Category With Such Name Alredy Exists");

            var Category = await unitOfWork.Categories.CreateAsync(categoryForCreationDTO.Adapt<Category>());
            await unitOfWork.SaveChangesAsync();

            return Category;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Category, bool>> expression)
        {

            if (!(await unitOfWork.Categories.DeleteAsync(expression)))
                throw new PharmacyException(404, "Category not found");

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<Category>> GetAllAsync(PaginationParams @params = null, Expression<Func<Category, bool>> expression = null)
        {
            var coffees = unitOfWork.Categories.GetAll(expression, null, false);

            if (@params != null)
                return await coffees.ToPagedList(@params).ToListAsync();

            return await coffees.ToListAsync();

        }

        public async ValueTask<Category> GetAsync(Expression<Func<Category, bool>> expression)
        {
          return  await unitOfWork.Categories.GetAsync(expression, null) ?? throw new PharmacyException(404, "Category not found");
        }

        public async ValueTask<Category> UpdateAsync(int id, CategoryForCreationDTO categoryForCreationDTO)
        {
            var alreadyExists = await unitOfWork.Categories.GetAsync(
             c => c.Content == categoryForCreationDTO.Content && c.Id != id);

            if (alreadyExists != null)
                throw new PharmacyException(400, "Category With Such Name Alredy Exists");

            var category = await GetAsync(c => c.Id == id);


            category.UpdatedAt = DateTime.UtcNow;

            category = unitOfWork.Categories.Update(categoryForCreationDTO.Adapt(category));
            await unitOfWork.SaveChangesAsync();

                return category;
        }
    }
}
