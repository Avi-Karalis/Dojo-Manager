using DojoManager.Data;
using DojoManager.Models;
using DojoManager.Services;
using Microsoft.AspNetCore.Mvc;
using DojoManager.ViewModels;

namespace DojoManager.Controllers {
    public class AttendancesController : Controller {

        private readonly IAttendanceService _attendanceService;
        private readonly IStudentService _studentService;
        private readonly ISessionService _sessionService;

        public AttendancesController(IAttendanceService attendanceService, ISessionService sessionService, IStudentService studentService)
            => (_attendanceService, _sessionService, _studentService) = (attendanceService, sessionService, studentService);


        public async Task<IActionResult> Index() {
            var attendances = await _attendanceService.GetAll();
            return View(attendances);
        }

        public async Task<IActionResult> ByStudent(int id) {
            var data = await _attendanceService.GetByStudent(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int studentId, int sessionId) {
            await _attendanceService.Add(studentId, sessionId);
            return RedirectToAction("ByStudent", new { id = studentId });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int studentId, int sessionId) {
            await _attendanceService.Remove(studentId, sessionId);
            return RedirectToAction("ByStudent", new { id = studentId });
        }

        public async Task<IActionResult> Create(int? sessionId) {
            var vm = new AddAttendanceViewModel {
                SessionId = sessionId ?? 0,
                Students = await _studentService.GetAll(),
                Sessions = await _sessionService.GetAll()
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddAttendanceViewModel vm) {
            await _attendanceService.Add(vm.StudentId, vm.SessionId);

            return RedirectToAction("Details", "Sessions", new { id = vm.SessionId });
        }
    }
}
