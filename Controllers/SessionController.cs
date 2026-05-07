using DojoManager.Models;
using DojoManager.ViewModels;
using DojoManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DojoManager.Controllers {

    public class SessionsController : Controller {
        private readonly ISessionService _service;

        public SessionsController(ISessionService service) {
            _service = service;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? sortBy = "date", string? sortDir = "desc") {
            (List<Session>? sessions, int totalCount) = await _service.GetPaged(page, pageSize, sortBy, sortDir);
            SessionIndexViewModel? viewModel = new()
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

        [Authorize]
        public IActionResult Create() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Session session) {
            if (!ModelState.IsValid)
                return View(session);

            await _service.Create(session);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id) {
            Session? session = await _service.GetById(id);
            if (session == null) return NotFound();

            return View(session);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) {
            Session? session = await _service.Details(id);

            if (session == null) return NotFound();

            return View(session);
        }
    }
}
