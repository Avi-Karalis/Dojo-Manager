using DojoManager.Models;

namespace DojoManager.Services {
    public interface IAttendanceService {
        Task<List<Attendance>> GetAll();
        Task<(List<Attendance> Attendances, int TotalCount)> GetPaged(int page, int pageSize, string? sortBy, string? sortDir);
        Task<List<Attendance>> GetByStudent(int studentId);
        Task<(List<Attendance> Attendances, int TotalCount)> GetByStudentPaged(int studentId, int page, int pageSize, string? sortBy, string? sortDir);
        Task Add(int studentId, int sessionId);
        Task Remove(int studentId, int sessionId);
    }
}
