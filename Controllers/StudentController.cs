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
            try
            {
                await _studentService.Create(student);
                TempData["Success"] = $"{student.Name} was created successfully.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to create student. Please try again.";
                return View(student);
            }
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

            try
            {
                var result = await _studentService.Update(student);
                if (result == null) return NotFound();
                TempData["Success"] = $"{student.Name} was updated successfully.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to save changes. Please try again.";
                return View(student);
            }
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
            try
            {
                var deleted = await _studentService.Delete(id);
                if (!deleted) return NotFound();
                TempData["Success"] = "Student deleted successfully.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to delete student. They may have attendance records that must be removed first.";
            }
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
            bool result = await _studentService.MarkAsPaid(id);
            if (!result) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
