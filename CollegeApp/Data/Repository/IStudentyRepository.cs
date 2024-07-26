namespace CollegeApp.Data.Repository
{
    public interface IStudentyRepository
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetByIdAsync(int id, bool useNoTracking = false);
        Task<Student> GetByNameAsync(string name);
        Task<int> CreateAsync(Student student);
        Task<int> UpdateAsync(Student student);
        Task<bool> DeleteAsync(Student student);
    }
}
