using DojoManager.Models;

namespace DojoManager.Services {
    public interface IStudentService {
        Task<List<Student>> GetAll();
        Task<(List<Student> Students, int TotalCount)> GetPaged(int page, int pageSize, string? sortBy, string? sortDir);
        Task<Student?> GetById(int id);
        Task<Student> Create(Student student);
        Task<Student> Update(Student student);
        Task<bool> Delete(int id);
        Task<bool> MarkAsPaid(int id);
    }
}
