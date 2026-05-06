using DojoManager.Data;
using DojoManager.Models;
using DojoManager.Services;
using Microsoft.AspNetCore.Mvc;
namespace DojoManager.Controllers {
    public class StudentsController : Controller {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService) {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index() {
            List<Student> students = await _studentService.GetAll();
            return View(students);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student) {
            if (!ModelState.IsValid) {
                return View(student);
            }
            await _studentService.Create(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id) {
            var student = await _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student) {
            if (!ModelState.IsValid)
                return View(student);

            await _studentService.Update(student);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id) {
            var student = await _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _studentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) {
            var student = await _studentService.GetById(id);
            if (student == null) return NotFound();

            return View(student);
        }
    }
}
