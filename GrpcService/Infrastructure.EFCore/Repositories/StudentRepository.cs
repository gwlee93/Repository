using Application.Persistences;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Repositories
{
    public class StudentRepository : IStudentRepository
    { 
        private readonly StudentDbContext _dbContext;
        public StudentRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Student> CreateAsync(Student entity, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Students.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }   

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken); 
            if(entity is not null)
            {
                _dbContext.Students.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Student>> FindAllAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
        {
            var entities = await _dbContext.Students.Where(student => ids.Contains(student.StudentId)).ToListAsync(cancellationToken);
            return entities;
        }

        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Students.OrderBy(student => student.StudentId).ToListAsync();
        }

        public async Task<Student> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Students.FindAsync(new object[] { id }, cancellationToken);
            if (student is null)
                throw new ArgumentNullException(nameof(Student));
            return student;
        }

        public async Task<Student> UpdateAsync(Student entity, CancellationToken cancellationToken = default)
        {
            var result =  _dbContext.Students.Update(entity).Entity;
            await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
