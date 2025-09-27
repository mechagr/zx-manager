using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZXManager.Data;
using ZXManager.Models;

namespace ZXManager.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ZXContext _context;

        public GamesController(ZXContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";
            ViewData["PublisherSortParm"] = sortOrder == "Publisher" ? "publisher_desc" : "Publisher";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "rating_desc" : "Rating";

            var games = from v in _context.Games.Include(v => v.Publisher).Include(v => v.Genre)
                        select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.Contains(searchString) ||
                                        s.Genre.Name.Contains(searchString) ||
                                        s.Publisher.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    games = games.OrderByDescending(v => v.Title);
                    break;
                case "Year":
                    games = games.OrderBy(v => v.Year);
                    break;
                case "year_desc":
                    games = games.OrderByDescending(v => v.Year);
                    break;
                case "Publisher":
                    games = games.OrderBy(v => v.Publisher.Name);
                    break;
                case "publisher_desc":
                    games = games.OrderByDescending(v => v.Publisher.Name);
                    break;
                case "Genre":
                    games = games.OrderBy(v => v.Genre.Name);
                    break;
                case "genre_desc":
                    games = games.OrderByDescending(v => v.Genre.Name);
                    break;
                case "Rating":
                    games = games.OrderBy(v => v.Rating);
                    break;
                case "rating_desc":
                    games = games.OrderByDescending(v => v.Rating);
                    break;
                default:
                    games = games.OrderBy(v => v.Title);
                    break;
            }

            return View(await games.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(v => v.Publisher)
                .Include(v => v.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        public IActionResult Create()
        {
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Year,Condition,Rating,PublisherId,GenreId")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", game.GenreId);
            return View(game);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();

            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", game.GenreId);
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,Condition,Rating,PublisherId,GenreId")] Game game)
        {
            if (id != game.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", game.GenreId);
            return View(game);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(v => v.Publisher)
                .Include(v => v.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}