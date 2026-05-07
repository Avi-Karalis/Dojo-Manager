using DojoManager.Data;
using DojoManager.Models;
using Microsoft.EntityFrameworkCore;
namespace DojoManager.Services {
    public class AttendanceService : IAttendanceService{
        private readonly AppDbContext _context;

        public AttendanceService(AppDbContext context) => _context = context;
            
        public async Task<List<Attendance>> GetAll() => await _context.Attendances.Include(a => a.Student).Include(a => a.Session).ToListAsync();

        public async Task<(List<Attendance> Attendances, int TotalCount)> GetPaged(int page, int pageSize, string? sortBy, string? sortDir) {
            var query = _context.Attendances.Include(a => a.Student).Include(a => a.Session).AsNoTracking();
            query = ApplySort(query, sortBy, sortDir);
            int total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<List<Attendance>> GetByStudent(int studentId) => await _context.Attendances.Include(a => a.Student).Include(a => a.Session).Where(a => a.StudentId == studentId).ToListAsync();

        public async Task<(List<Attendance> Attendances, int TotalCount)> GetByStudentPaged(int studentId, int page, int pageSize, string? sortBy, string? sortDir) {
            var query = _context.Attendances.Include(a => a.Student).Include(a => a.Session).Where(a => a.StudentId == studentId).AsNoTracking();
            query = ApplySort(query, sortBy, sortDir);
            int total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        private static IQueryable<Attendance> ApplySort(IQueryable<Attendance> query, string? sortBy, string? sortDir) =>
            (sortBy?.ToLower(), sortDir?.ToLower()) switch {
                ("student", "desc") => query.OrderByDescending(a => a.Student!.Name),
                ("student", _)     => query.OrderBy(a => a.Student!.Name),
                ("date", "desc")   => query.OrderByDescending(a => a.Session!.Date),
                ("date", _)        => query.OrderBy(a => a.Session!.Date),
                ("location", "desc") => query.OrderByDescending(a => a.Session!.Location),
                ("location", _)    => query.OrderBy(a => a.Session!.Location),
                _                  => query.OrderByDescending(a => a.Session!.Date)
            };

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
