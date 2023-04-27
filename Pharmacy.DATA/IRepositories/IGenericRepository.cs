using System.Linq.Expressions;
using Pharmacy.Domain.Commons;

namespace Pharmacy.Data.IRepositories
{
    public interface IGenericRepository<T> where T : Auditable
    {
        ValueTask<T> CreateAsync(T entity);
        T Update(T entity);
        ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression,
            string[] includes = null,
            bool isTracking = true);
        ValueTask SaveChangesAsync();
    }
}
