using DojoManager.Data;
using DojoManager.Models;
using Microsoft.EntityFrameworkCore;
namespace DojoManager.Services {
    public class AttendanceService : IAttendanceService{
        private readonly AppDbContext _context;

        public AttendanceService(AppDbContext context) => _context = context;
            
        public async Task<List<Attendance>> GetAll() => await _context.Attendances.Include(a => a.Student).Include(a => a.Session).ToListAsync();
        
        public async Task<List<Attendance>> GetByStudent(int studentId) => await _context.Attendances.Include(a => a.Student).Include(a => a.Session).Where(a => a.StudentId == studentId).ToListAsync();

        public async Task Add(int studentId, int sessionId) {
            var exists = await _context.Attendances.AnyAsync(a => a.StudentId == studentId && a.SessionId == sessionId);
            
            if (exists) return;

            _context.Attendances.Add(new Attendance { StudentId = studentId, SessionId = sessionId });

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int studentId, int sessionId) {
            var attendance = await _context.Attendances.FindAsync(studentId, sessionId);

            if (attendance == null) return;
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
        }

    }
}
