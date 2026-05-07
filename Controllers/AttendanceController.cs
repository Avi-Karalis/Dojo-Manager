using DojoManager.Data;
using DojoManager.Models;
using DojoManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DojoManager.ViewModels;

namespace DojoManager.Controllers {
    [Authorize]
    public class AttendancesController : Controller {

        private readonly IAttendanceService _attendanceService;
        private readonly IStudentService _studentService;
        private readonly ISessionService _sessionService;

        public AttendancesController(IAttendanceService attendanceService, ISessionService sessionService, IStudentService studentService)
            => (_attendanceService, _sessionService, _studentService) = (attendanceService, sessionService, studentService);


        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? sortBy = "date", string? sortDir = "desc") {
            (List<Attendance>? attendances, int total) = await _attendanceService.GetPaged(page, pageSize, sortBy, sortDir);
            AttendanceIndexViewModel? vm = new()
            {
                Attendances = attendances,
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                SortBy = sortBy,
                SortDir = sortDir
            };
            return View(vm);
        }

        public async Task<IActionResult> ByStudent(int id, int page = 1, int pageSize = 10, string? sortBy = "date", string? sortDir = "desc") {
            (List<Attendance>? attendances, int total) = await _attendanceService.GetByStudentPaged(id, page, pageSize, sortBy, sortDir);
            AttendanceIndexViewModel? vm = new () {
                Attendances = attendances,
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                SortBy = sortBy,
                SortDir = sortDir,
                StudentId = id
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int studentId, int sessionId) {
            await _attendanceService.Add(studentId, sessionId);
            return RedirectToAction("ByStudent", new { id = studentId });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int studentId, int sessionId) {
            await _attendanceService.Remove(studentId, sessionId);
            return RedirectToAction("Details", "Sessions", new { id = sessionId });
        }

        public async Task<IActionResult> Create(int? sessionId) {
            AddAttendanceViewModel? vm = new AddAttendanceViewModel {
                SessionId = sessionId ?? 0,
                Students = await _studentService.GetAll(),
                Sessions = await _sessionService.GetAll()
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddAttendanceViewModel vm)
        {
            if (vm.SessionId == 0)
            {
                TempData["Error"] = "Please select a session.";
                vm.Students = await _studentService.GetAll();
                vm.Sessions = await _sessionService.GetAll();
                return View(vm);
            }
            if (vm.StudentIds.Count == 0)
            {
                TempData["Error"] = "Please select at least one student.";
                vm.Students = await _studentService.GetAll();
                vm.Sessions = await _sessionService.GetAll();
                return View(vm);
            }
            try
            {
                foreach (int studentId in vm.StudentIds)
                    await _attendanceService.Add(studentId, vm.SessionId);
                TempData["Success"] = $"{vm.StudentIds.Count} attendance record(s) added.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to add attendance. Please try again.";
            }
            return RedirectToAction("Details", "Sessions", new { id = vm.SessionId });
        }
    }
}
