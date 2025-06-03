using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NetDemo.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbContext;
        private DbSet<T> _dbSet;
        public CollegeRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T record)
        {
            _dbSet.Add(record);
            await _dbContext.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteAsync(T record)
        {
            _dbSet.Remove(record);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T record)
        {
            _dbSet.Update(record);

            await _dbContext.SaveChangesAsync();
            return record;
        }
    }
}
