using DojoManager.Data;
using DojoManager.Models;
using Microsoft.EntityFrameworkCore;
namespace DojoManager.Services
{
    public class SessionService : ISessionService
    {

        private readonly AppDbContext _context;

        public SessionService(AppDbContext context) => _context = context;

        public async Task<List<Session>> GetAll() => await _context.Sessions.AsNoTracking().ToListAsync();

        public async Task<(List<Session> Sessions, int TotalCount)> GetPaged(int page, int pageSize, string? sortBy, string? sortDir)
        {
            IQueryable<Session> query = _context.Sessions.AsNoTracking();
            query = ApplySort(query, sortBy, sortDir);
            int total = await query.CountAsync();
            List<Session>? items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
        private static IQueryable<Session> ApplySort(IQueryable<Session> query, string? sortBy, string? sortDir) =>
            (sortBy?.ToLower(), sortDir?.ToLower()) switch{
                ("date", "desc") => query.OrderByDescending(s => s.Date),
                ("date", _) => query.OrderBy(s => s.Date),
                _ => query.OrderByDescending(s => s.Date)
            };
        public async Task<Session?> GetById(int id)
        {
            return await _context.Sessions
                .Include(s => s.Attendances)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Session> Create(Session session)
        {
            session.Date = DateTime.SpecifyKind(session.Date, DateTimeKind.Utc);

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return session;
        }

        public async Task<bool> Delete(int id)
        {
            Session? session = await _context.Sessions.FindAsync(id);
            if (session == null) return false;

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Session> Details(int id)
        {
            Session? session = await _context.Sessions
                .Include(s => s.Attendances)
                    .ThenInclude(a => a.Student)
                .FirstOrDefaultAsync(s => s.Id == id);

            return session;
        }
    }
}
