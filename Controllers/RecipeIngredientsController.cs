using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RecipeIngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipeIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecipeIngredients
        public async Task<IActionResult> Index()
        {
            var recipeIngredients = await _context.RecipeIngredients
                .Include(ri => ri.Ingredient)
                .Include(ri => ri.Recipe)
                .ToListAsync();

            return View(recipeIngredients);
        }

        // GET: RecipeIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Create
        public IActionResult Create()
        {
            var ingredients = _context.Ingredients.ToList();
            var recipes = _context.Recipes.ToList();

            if (ingredients == null || recipes == null)
            {
                // Обработка случая, когда ингредиенты или рецепты отсутствуют
                return View("Error"); // Или другой подходящий метод обработки
            }

            ViewData["IngredientID"] = new SelectList(ingredients, "ID", "IngredientName");
            ViewData["RecipeID"] = new SelectList(recipes, "ID", "RecipeName");
            return View();
        }

        // POST: RecipeIngredients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeID,IngredientID,Quantity")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RecipeID"] = new SelectList(_context.Recipes, "ID", "RecipeName", recipeIngredient.RecipeID);
            ViewData["IngredientID"] = new SelectList(_context.Ingredients, "ID", "IngredientName", recipeIngredient.IngredientID);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            ViewData["IngredientID"] = new SelectList(_context.Ingredients, "ID", "IngredientName", recipeIngredient.IngredientID);
            ViewData["RecipeID"] = new SelectList(_context.Recipes, "ID", "RecipeName", recipeIngredient.RecipeID);
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RecipeID,IngredientID,Quantity")] RecipeIngredient recipeIngredient)
        {
            if (id != recipeIngredient.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeIngredientExists(recipeIngredient.ID))
                             {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Повторно выбрасываем исключение для обработки в глобальном обработчике
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IngredientID"] = new SelectList(_context.Ingredients, "ID", "IngredientName", recipeIngredient.IngredientID);
            ViewData["RecipeID"] = new SelectList(_context.Recipes, "ID", "RecipeName", recipeIngredient.RecipeID);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient != null)
            {
                _context.RecipeIngredients.Remove(recipeIngredient);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeIngredientExists(int id)
        {
            return _context.RecipeIngredients.Any(e => e.ID == id);
        }
    }
}
