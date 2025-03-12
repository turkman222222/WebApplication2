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
    public class CokingStepsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CokingStepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CokingSteps
        public async Task<IActionResult> Index()
        {
            return View(await _context.CokingSteps.ToListAsync());
        }

        // GET: CokingSteps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cokingStep = await _context.CokingSteps
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cokingStep == null)
            {
                return NotFound();
            }

            return View(cokingStep);
        }

        // GET: CokingSteps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CokingSteps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ReceptID,StepNumber,StepDiscription")] CokingStep cokingStep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cokingStep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cokingStep);
        }

        // GET: CokingSteps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cokingStep = await _context.CokingSteps.FindAsync(id);
            if (cokingStep == null)
            {
                return NotFound();
            }
            return View(cokingStep);
        }

        // POST: CokingSteps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ReceptID,StepNumber,StepDiscription")] CokingStep cokingStep)
        {
            if (id != cokingStep.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cokingStep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CokingStepExists(cokingStep.ID))
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
            return View(cokingStep);
        }

        // GET: CokingSteps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cokingStep = await _context.CokingSteps
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cokingStep == null)
            {
                return NotFound();
            }

            return View(cokingStep);
        }

        // POST: CokingSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cokingStep = await _context.CokingSteps.FindAsync(id);
            if (cokingStep != null)
            {
                _context.CokingSteps.Remove(cokingStep);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CokingStepExists(int id)
        {
            return _context.CokingSteps.Any(e => e.ID == id);
        }
    }
}
