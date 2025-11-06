using Microsoft.EntityFrameworkCore;
using TRIAL.Persistence.entity;

namespace TRIAL.Persistence.Repository
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<HomeworkStudent> HwS { get; set; }
        public DbSet<HomeworkTeacher> HwT { get; set; }
        public DbSet<Marks> marks { get; set; }
        public DbSet<Subjects> subjectNa { get; set; }
        public DbSet<Registration> registrations { get; set; }
        public DbSet<EmailVerification> emailVerification { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}