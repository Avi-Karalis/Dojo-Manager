using DojoManager.Models;

namespace DojoManager.Services {
    public interface ISessionService {
        Task<List<Session>> GetAll();
        Task<Session?> GetById(int id);
        Task<Session> Create(Session session);
        Task<bool> Delete(int id);
        Task<Session> Details(int id);
    }
}
