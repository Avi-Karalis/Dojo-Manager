using DojoManager.Models;

namespace DojoManager.Services {
    public interface ISessionService {
        Task<List<Session>> GetAll();
        Task<(List<Session> Sessions, int TotalCount)> GetPaged(int page, int pageSize, string? sortBy, string? sortDir);
        Task<Session?> GetById(int id);
        Task<Session> Create(Session session);
        Task<bool> Delete(int id);
        Task<Session> Details(int id);
    }
}
