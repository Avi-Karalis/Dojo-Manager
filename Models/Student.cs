namespace DojoManager.Models {
    public class Student {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rank { get; set; }

        public List<Attendance>? Attendances { get; set; } = new();
    }
}
