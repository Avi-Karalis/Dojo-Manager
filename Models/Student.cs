namespace DojoManager.Models {
    public class Student {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rank { get; set; }
        public bool IsVisitor { get; set; }
        public DateTime? LastPaidDate { get; set; }

        public bool HasPaidThisMonth =>
            LastPaidDate.HasValue &&
            LastPaidDate.Value.Year == DateTime.UtcNow.Year &&
            LastPaidDate.Value.Month == DateTime.UtcNow.Month;

        public List<Attendance>? Attendances { get; set; } = new();
    }
}
