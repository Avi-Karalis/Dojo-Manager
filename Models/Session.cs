using System;
namespace DojoManager.Models {
    public class Session {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public List<Attendance> Attendances { get; set; } = new();
    }
}
