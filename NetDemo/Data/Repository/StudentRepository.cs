
using Microsoft.EntityFrameworkCore;

namespace NetDemo.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Student> CreateAsync(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student> DeleteAsync(int id)
        {
            var studentToDelete = await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            if (studentToDelete == null)
                throw new ArgumentException($"Not found student with id: {id}");
            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
            return studentToDelete;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbContext.Students.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();
            return await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Student> UpdateAsync(int id, Student student)
        {
            _dbContext.Update(student);

            await _dbContext.SaveChangesAsync();
            return student;
        }
    }
}
