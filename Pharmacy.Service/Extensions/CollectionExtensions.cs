using Pharmacy.Domain.Configurations;

namespace Pharmacy.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
        {
            return source.Skip((@params.PageIndex - 1) * @params.PageSize)
                  .Take(@params.PageSize);
        }
    }
}
