using DojoManager.Models;
namespace DojoManager.Models {
    public class Attendance {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int SessionId { get; set; }
        public Session? Session { get; set; }

    }
}
