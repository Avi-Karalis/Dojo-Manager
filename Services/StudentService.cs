using DojoManager.Data;
using DojoManager.Models;
using Microsoft.EntityFrameworkCore;
namespace DojoManager.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context) => _context = context;

        public async Task<List<Student>> GetAll() => await _context.Students.AsNoTracking().ToListAsync();

        public async Task<(List<Student> Students, int TotalCount)> GetPaged(int page, int pageSize, string? sortBy, string? sortDir) {
            var query = _context.Students.AsNoTracking();

            query = (sortBy?.ToLower(), sortDir?.ToLower()) switch {
                ("name", "desc")         => query.OrderByDescending(s => s.Name),
                ("name", _)             => query.OrderBy(s => s.Name),
                ("rank", "desc")        => query.OrderByDescending(s => s.Rank),
                ("rank", _)             => query.OrderBy(s => s.Rank),
                ("isvisitor", "desc")   => query.OrderByDescending(s => s.IsVisitor),
                ("isvisitor", _)        => query.OrderBy(s => s.IsVisitor),
                ("lastpaiddate", "desc")=> query.OrderByDescending(s => s.LastPaidDate),
                ("lastpaiddate", _)     => query.OrderBy(s => s.LastPaidDate),
                _                       => query.OrderBy(s => s.Name)
            };

            int total = await query.CountAsync();
            var students = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (students, total);
        }

        public async Task<Student?> GetById(int id) => await _context.Students.FindAsync(id);

        public async Task<Student> Create(Student student)
        {
            if (student.LastPaidDate.HasValue)
                student.LastPaidDate = DateTime.SpecifyKind(student.LastPaidDate.Value, DateTimeKind.Utc);
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }
        public async Task<Student?> Update(Student student)
        {
            var existing = await _context.Students.FindAsync(student.Id);
            if (existing == null) return null;

            if (student.LastPaidDate.HasValue)
                student.LastPaidDate = DateTime.SpecifyKind(student.LastPaidDate.Value, DateTimeKind.Utc);

            _context.Entry(existing).CurrentValues.SetValues(student);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> MarkAsPaid(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            student.LastPaidDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
