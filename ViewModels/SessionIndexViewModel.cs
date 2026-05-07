using DojoManager.Models;

namespace DojoManager.ViewModels {
    public class SessionIndexViewModel
    {
        public List<Session> Sessions { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string? SortBy { get; set; }
        public string? SortDir { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrev => Page > 1;
        public bool HasNext => Page < TotalPages;

        public string NextDir(string column) =>
            SortBy == column && SortDir == "asc" ? "desc" : "asc";
        public string SortIndicator(string column) =>
            SortBy == column ? (SortDir == "asc" ? " ▲" : " ▼") : "";
    }
}
