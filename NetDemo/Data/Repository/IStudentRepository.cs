namespace NetDemo.Data.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id, bool useNoTracking = false);

        Task<Student> CreateAsync(Student student);
        Task<Student> UpdateAsync(int id, Student student);
        Task<Student> DeleteAsync(int id);
    }
}
