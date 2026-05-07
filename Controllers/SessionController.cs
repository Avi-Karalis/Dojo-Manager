using DojoManager.Models;
using DojoManager.ViewModels;
using DojoManager.Services;
using Microsoft.AspNetCore.Mvc;
namespace DojoManager.Controllers {

    public class SessionsController : Controller {
        private readonly ISessionService _service;

        public SessionsController(ISessionService service) {
            _service = service;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? sortBy = "date", string? sortDir = "desc") {
            var (sessions, totalCount) = await _service.GetPaged(page, pageSize, sortBy, sortDir);
            var viewModel = new SessionIndexViewModel
            {
                Sessions = sessions,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                SortBy = sortBy,
                SortDir = sortDir
            };
            return View(viewModel);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Session session) {
            if (!ModelState.IsValid)
                return View(session);

            await _service.Create(session);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) {
            var session = await _service.GetById(id);
            if (session == null) return NotFound();

            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) {
            var session = await _service.Details(id);

            if (session == null) return NotFound();

            return View(session);
        }
    }
}
