using DojoManager.Models;
using Microsoft.EntityFrameworkCore;
namespace DojoManager.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.StudentId, a.SessionId });
        }
    }
}