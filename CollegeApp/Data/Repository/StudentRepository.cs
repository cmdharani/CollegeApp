
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data.Repository
{
    public class StudentRepository :CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext):base(dbContext) 
        {

            _dbContext = dbContext;

        }

        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus)
        {
            //Write code to return students having fee status pending
            return null;
        }



        #region Commented due to CommonRepository is having it
        //public async Task<int> CreateAsync(Student student)
        //{
        //    _dbContext.Students.Add(student);
        //    await _dbContext.SaveChangesAsync();
        //    return student.Id;
        //}

        //public async Task<bool> DeleteAsync(Student student)
        //{
        //    _dbContext.Students.Remove(student);
        //    await _dbContext.SaveChangesAsync();

        //    return true;
        //}

        //public async Task<List<Student>> GetAllStudentsAsync()
        //{
        //    return await _dbContext.Students.ToListAsync();
        //}

        //public async Task<Student> GetByIdAsync(int id, bool useNoTracking = false)
        //{
        //    if (useNoTracking)
        //        return await _dbContext.Students.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();
        //    else
        //        return await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();

        //}

        //public async Task<Student> GetByNameAsync(string name)
        //{
        //    return await _dbContext.Students.FirstOrDefaultAsync(x => x.StudentName == name);
        //}

        //public async Task<int> UpdateAsync(Student student)
        //{
        //    _dbContext.Update(student);
        //    await _dbContext.SaveChangesAsync();

        //    return student.Id;
        //} 
        #endregion
    }
}
