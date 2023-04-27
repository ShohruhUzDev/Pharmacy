using System.Linq.Expressions;
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
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask<Medicine> CreateAsync(MedicineForCreationDTO PharmacyForCreationDTO)
        {
            var alreadyExists = await unitOfWork.Medicines.GetAsync(
                c => c.Name == PharmacyForCreationDTO.Name);

            if (alreadyExists != null)
                throw new PharmacyException(400, "Medicine With Such Name Alredy Exists");

            var Pharmacy = await unitOfWork.Medicines.CreateAsync(PharmacyForCreationDTO.Adapt<Medicine>());
            await unitOfWork.SaveChangesAsync();

            return Pharmacy;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Medicine, bool>> expression)
        {
            if (!(await unitOfWork.Medicines.DeleteAsync(expression)))
                throw new PharmacyException(404, "Medicine not found");

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<Medicine>> GetAllAsync(PaginationParams @params = null, Expression<Func<Medicine, bool>> expression = null)
        {
            var Pharmacys = unitOfWork.Medicines.GetAll(expression, new string[] { "Attachment" }, false);

            if (@params != null)
                return await Pharmacys.ToPagedList(@params).ToListAsync();

            return await Pharmacys.ToListAsync();
        }

        public async ValueTask<Medicine> GetAsync(Expression<Func<Medicine, bool>> expression) =>
            await unitOfWork.Medicines.GetAsync(expression, new string[] { "Attachment" }) ?? throw new PharmacyException(404, "Pharmacy not found");

        public async ValueTask<Medicine> UpdateAsync(int id, MedicineForCreationDTO PharmacyForCreationDTO)
        {
            var alreadyExists = await unitOfWork.Medicines.GetAsync(
                c => c.Name == PharmacyForCreationDTO.Name && c.Id != id);

            if (alreadyExists != null)
                throw new PharmacyException(400, "Medicine With Such Name Alredy Exists");

            var Pharmacy = await GetAsync(c => c.Id == id);


            Pharmacy.UpdatedAt = DateTime.UtcNow;

            Pharmacy = unitOfWork.Medicines.Update(PharmacyForCreationDTO.Adapt(Pharmacy));
            await unitOfWork.SaveChangesAsync();

            return Pharmacy;
        }
    }
}
