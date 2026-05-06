using DojoManager.Models;

namespace DojoManager.ViewModels {
    public class AddAttendanceViewModel {
        public int StudentId { get; set; }
        public int SessionId { get; set; }

        public List<Student>? Students { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}
