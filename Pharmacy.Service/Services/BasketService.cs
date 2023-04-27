using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data.IRepositories;
using Pharmacy.Domain.Configurations;
using Pharmacy.Domain.Entities.Orders;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Exceptions;
using Pharmacy.Service.Extensions;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.Service.Services
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork unitOfWork;
        public BasketService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async ValueTask<Basket> CreateAsync(BasketForCreationDTO basketForCreationDTO)
        {

            var Basket = await unitOfWork.Baskets.CreateAsync(basketForCreationDTO.Adapt<Basket>());
            await unitOfWork.SaveChangesAsync();

            return Basket;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Basket, bool>> expression)
        {

            if (!(await unitOfWork.Baskets.DeleteAsync(expression)))
                throw new PharmacyException(404, "Basket not found");

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<Basket>> GetAllAsync(PaginationParams @params = null,
            Expression<Func<Basket, bool>> expression = null)
        {
            var baskets = unitOfWork.Baskets.GetAll(expression,null, false);


            if (@params != null)
                return await baskets.ToPagedList(@params).ToListAsync();

            return await baskets.ToListAsync();

        }

        public async ValueTask<Basket> GetAsync(Expression<Func<Basket, bool>> expression)
        {
            return await unitOfWork.Baskets.GetAsync(expression,
                    null ) ?? throw new PharmacyException(404, "Basket not found");
        }

        public async ValueTask<Basket> UpdateAsync(int id, BasketForCreationDTO basketForCreationDTO)
        {

            var basket = await GetAsync(c => c.Id == id);


            basket.UpdatedAt = DateTime.UtcNow;

            basket = unitOfWork.Baskets.Update(basketForCreationDTO.Adapt(basket));
            await unitOfWork.SaveChangesAsync();

            return basket;
        }
    }
}
