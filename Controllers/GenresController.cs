using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZXManager.Data;
using ZXManager.Models;

namespace ZXManager.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly ZXContext _context;

        public GenresController(ZXContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genres
                .Include(g => g.Games)
                .ToListAsync();
            return View(genres);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var genre = await _context.Genres
                .Include(g => g.Games)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Genre genre)
        {
            if (!ModelState.IsValid) return View(genre);

            _context.Add(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Genre genre)
        {
            if (id != genre.Id) return NotFound();
            if (!ModelState.IsValid) return View(genre);

            try
            {
                _context.Update(genre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(genre.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id) => _context.Genres.Any(e => e.Id == id);
    }
}
