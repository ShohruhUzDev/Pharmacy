using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache cache;
        private readonly IUnitOfWork unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork , IDistributedCache cache )
        {
            this.cache = cache;
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

            await InvalidateTheCache();

            return Category;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Category, bool>> expression)
        {

            if (!(await unitOfWork.Categories.DeleteAsync(expression)))
                throw new PharmacyException(404, "Category not found");

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IQueryable<Category>> GetAllAsync(PaginationParams @params = null, Expression<Func<Category, bool>> expression = null)
        {

            var categories = await GetFromCache();

            if(categories is null)
            {
                categories = unitOfWork.Categories.GetAll(expression, null, false);

                if(categories is not null && categories.Any())
                {
                    await SaveToCache(categories);
                }
            }

           
            if (@params != null)
                return  categories.Include(cat=>cat.Medicines);

            return categories.Include(cat => cat.Medicines);

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
        private async Task<IQueryable<Category>> GetFromCache()
        {
            IQueryable<Category> categories = null;
            try
            {
                var categoriesFromCache = await cache.GetStringAsync(typeof(Category).FullName);
                categories = (categoriesFromCache == null) ? null
                    : JsonSerializer.Deserialize<IQueryable<Category>>(categoriesFromCache);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync( ex.Message);
                //   _logger.LogError(ex, $"Exception occurred in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }

            return categories;
        }
        private async Task SaveToCache(IQueryable<Category> categories)
        {
            try
            {
                await cache.SetStringAsync(typeof(Category).FullName, JsonSerializer.Serialize(categories), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            catch (Exception ex)
            {

                await Console.Out.WriteLineAsync($"Exception occurred in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
        private async Task InvalidateTheCache()
        {
            try
            {
                await cache.RemoveAsync(typeof(Category).FullName);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Exception occurred in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
    }
}
