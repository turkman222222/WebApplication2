using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class RecipesController : Controller
{
    private readonly ApplicationDbContext _context;

    public RecipesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Recipes
    public async Task<IActionResult> Index(string searchString, int? categoryId)
    {
        var recipes = _context.Recipes.Include(r => r.Author).Include(r => r.Category).AsQueryable();

        // Фильтрация по категории
        if (categoryId.HasValue)
        {
            recipes = recipes.Where(r => r.CategoryID == categoryId);
        }

        // Поиск по названию
        if (!string.IsNullOrEmpty(searchString))
        {
            recipes = recipes.Where(r => r.RecipeName.Contains(searchString));
        }

        // Получаем список категорий для выпадающего списка
        ViewBag.CategoryID = await _context.Categories.ToListAsync();

        return View(await recipes.ToListAsync());
    }

    // GET: Recipes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .Include(r => r.Author)
            .Include(r => r.Category)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: Recipes/Create
    public IActionResult Create()
    {
        ViewData["AuthorID"] = new SelectList(_context.IDAuthor, "ID", "Authorname");
        ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName");
        return View();
    }

    // POST: Recipes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,RecipeName,Discription,CategoryID,AuthorID,CookingTime")] Recipe recipe, IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                recipe.Image = memoryStream.ToArray(); // Сохраняем изображение как массив байтов
            }
        }

        _context.Add(recipe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

        ViewData["AuthorID"] = new SelectList(_context.IDAuthor, "ID", "Authorname", recipe.AuthorID);
        ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", recipe.CategoryID);
        return View(recipe);
    }

    // GET: Recipes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        ViewData["AuthorID"] = new SelectList(_context.IDAuthor, "ID", "Authorname", recipe.AuthorID);
        ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", recipe.CategoryID);
        return View(recipe);
    }

    // POST: Recipes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,RecipeName,Discription,CategoryID,AuthorID,CookingTime")] Recipe recipe, IFormFile imageFile)
    {
        if (id != recipe.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        recipe.Image = memoryStream.ToArray(); // Обновляем изображение
                    }
                }

                _context.Update(recipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(recipe.ID))
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
        ViewData["AuthorID"] = new SelectList(_context.IDAuthor, "ID", "Authorname", recipe.AuthorID);
        ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", recipe.CategoryID);
        return View(recipe);
    }

    // GET: Recipes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _context.Recipes
            .Include(r => r.Author)
            .Include(r => r.Category)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: Recipes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RecipeExists(int id)
    {
        return _context.Recipes.Any(e => e.ID == id);
    }
    public IActionResult GetImage(int id)
    {
        var recipe = _context.Recipes.Find(id);
        if (recipe != null && recipe.Image != null)
        {
            return File(recipe.Image, "image/jpeg"); // Убедитесь, что тип изображения соответствует вашему
        }
        return NotFound();
    }


}
