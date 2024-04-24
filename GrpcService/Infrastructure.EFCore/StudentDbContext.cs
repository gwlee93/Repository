using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;        
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Student");

            builder.Entity<Student>().HasData(
                new Student("1", "이건우", 32,"C#"),
                new Student("2", "안성윤", 32, "JAVA"),
                new Student("3", "장동계", 32, "JAVASCRIPT")
                );            
        }
    }
}
