using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RecipeTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipeTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecipeTags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RecipeTags.Include(r => r.Teg);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RecipeTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeTag = await _context.RecipeTags
                .Include(r => r.Teg)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeTag == null)
            {
                return NotFound();
            }

            return View(recipeTag);
        }

        // GET: RecipeTags/Create
        public IActionResult Create()
        {
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Id");
            return View();
        }

        // POST: RecipeTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PecipeID,TagId")] RecipeTag recipeTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Id", recipeTag.TagId);
            return View(recipeTag);
        }

        // GET: RecipeTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeTag = await _context.RecipeTags.FindAsync(id);
            if (recipeTag == null)
            {
                return NotFound();
            }
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Id", recipeTag.TagId);
            return View(recipeTag);
        }

        // POST: RecipeTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PecipeID,TagId")] RecipeTag recipeTag)
        {
            if (id != recipeTag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeTagExists(recipeTag.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Id", recipeTag.TagId);
            return View(recipeTag);
        }

        // GET: RecipeTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeTag = await _context.RecipeTags
                .Include(r => r.Teg)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeTag == null)
            {
                return NotFound();
            }

            return View(recipeTag);
        }

        // POST: RecipeTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeTag = await _context.RecipeTags.FindAsync(id);
            if (recipeTag != null)
            {
                _context.RecipeTags.Remove(recipeTag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeTagExists(int id)
        {
            return _context.RecipeTags.Any(e => e.ID == id);
        }
    }
}
