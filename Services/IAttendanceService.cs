using DojoManager.Models;

namespace DojoManager.Services {
    public interface IAttendanceService {
        Task<List<Attendance>> GetAll();
        Task<List<Attendance>> GetByStudent(int studentId);
        Task Add(int studentId, int sessionId);
        Task Remove(int studentId, int sessionId);
    }
}
