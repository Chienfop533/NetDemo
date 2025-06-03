using System.Linq.Expressions;

namespace NetDemo.Data.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);

        Task<T> CreateAsync(T record);
        Task<T> UpdateAsync(T record);
        Task<bool> DeleteAsync(T record);
    }
}
