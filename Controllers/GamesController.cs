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
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";
            ViewData["PublisherSortParm"] = sortOrder == "Publisher" ? "publisher_desc" : "Publisher";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "rating_desc" : "Rating";

            var games = _context.Games.Include(g => g.Publisher).Include(g => g.Genre).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Title.Contains(searchString) ||
                                         g.Genre.Name.Contains(searchString) ||
                                         g.Publisher.Name.Contains(searchString));
            }

            games = sortOrder switch
            {
                "title_desc" => games.OrderByDescending(g => g.Title),
                "Year" => games.OrderBy(g => g.Year),
                "year_desc" => games.OrderByDescending(g => g.Year),
                "Publisher" => games.OrderBy(g => g.Publisher.Name),
                "publisher_desc" => games.OrderByDescending(g => g.Publisher.Name),
                "Genre" => games.OrderBy(g => g.Genre.Name),
                "genre_desc" => games.OrderByDescending(g => g.Genre.Name),
                "Rating" => games.OrderBy(g => g.Rating),
                "rating_desc" => games.OrderByDescending(g => g.Rating),
                _ => games.OrderBy(g => g.Title),
            };

            return View(await games.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(g => g.Publisher)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        public IActionResult Create()
        {
            PopulateDropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Year,Condition,Rating,PublisherId,GenreId")] Game game)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns(game);
                return View(game);
            }

            _context.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();

            PopulateDropDowns(game);
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,Condition,Rating,PublisherId,GenreId")] Game game)
        {
            if (id != game.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                PopulateDropDowns(game);
                return View(game);
            }

            try
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(game.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.Games
                .Include(g => g.Publisher)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(g => g.Id == id);

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

        private bool GameExists(int id) => _context.Games.Any(g => g.Id == id);

        private void PopulateDropDowns(Game game = null)
        {
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game?.PublisherId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", game?.GenreId);
        }
    }
}
