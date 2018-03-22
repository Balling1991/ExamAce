using ExamAce.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamAce.Data
{
    public class ExamAceContext : IdentityDbContext<User>
    {
        public ExamAceContext(DbContextOptions<ExamAceContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Module> Modules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseTeacher>().HasKey(ct => new { ct.CourseId, ct.TeacherId });
            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseTeacher)
                .HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<CourseTeacher>()
                .HasOne(ct => ct.Teacher)
                .WithMany(c => c.CourseTeacher)
                .HasForeignKey(ct => ct.TeacherId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
