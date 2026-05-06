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

        public async Task<Student?> GetById(int id) => await _context.Students.FindAsync(id);

        public async Task<Student> Create(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }
        public async Task<Student?> Update(Student student)
        {
            var existing = await _context.Students.FindAsync(student.Id);
            if (existing == null) return null;

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
