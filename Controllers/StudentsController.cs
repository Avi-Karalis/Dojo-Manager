using DojoManager.Data;
using DojoManager.Models;
using DojoManager.Services;
using DojoManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DojoManager.Controllers {
    public class StudentsController : Controller {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService) {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? sortBy = "name", string? sortDir = "asc") {
            (List<Student>? students, int total) = await _studentService.GetPaged(page, pageSize, sortBy, sortDir);
            StudentIndexViewModel? vm = new () {
                Students = students,
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                SortBy = sortBy,
                SortDir = sortDir
            };
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Create() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Student student) {
            if (!ModelState.IsValid) {
                return View(student);
            }
            await _studentService.Create(student);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id) {
            Student? student = await _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Student student) {
            if (!ModelState.IsValid)
                return View(student);

            await _studentService.Update(student);
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> Delete(int id) {
            Student? student = await _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _studentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) {
            Student? student = await _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MarkAsPaid(int id) {
            await _studentService.MarkAsPaid(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
