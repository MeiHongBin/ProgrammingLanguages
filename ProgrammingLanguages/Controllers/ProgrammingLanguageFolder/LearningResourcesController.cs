using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.Models;
using ProgrammingLanguages.ProgrammingLanguageModels;

namespace ProgrammingLanguages.Controllers.ProgrammingLanguageFolder
{
    public class LearningResourcesController : Controller
    {
        private readonly LanguageProjectContext _context;

        public LearningResourcesController(LanguageProjectContext context)
        {
            _context = context;
        }

        // GET: LearningResources
        public async Task<IActionResult> Index()
        {
            var languageProjectContext = _context.LearningResources.Include(l => l.Language);
            return View(await languageProjectContext.ToListAsync());
        }

        // GET: LearningResources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningResource = await _context.LearningResources
                .Include(l => l.Language)
                .FirstOrDefaultAsync(m => m.ResourceId == id);
            if (learningResource == null)
            {
                return NotFound();
            }

            return View(learningResource);
        }

        // GET: LearningResources/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId");
            return View();
        }

        // POST: LearningResources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResourceId,LanguageId,ResourceName,ResourceLink,ResourceType")] LearningResource learningResource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learningResource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", learningResource.LanguageId);
            return View(learningResource);
        }

        // GET: LearningResources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningResource = await _context.LearningResources.FindAsync(id);
            if (learningResource == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", learningResource.LanguageId);
            return View(learningResource);
        }

        // POST: LearningResources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResourceId,LanguageId,ResourceName,ResourceLink,ResourceType")] LearningResource learningResource)
        {
            if (id != learningResource.ResourceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningResource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningResourceExists(learningResource.ResourceId))
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
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", learningResource.LanguageId);
            return View(learningResource);
        }

        // GET: LearningResources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningResource = await _context.LearningResources
                .Include(l => l.Language)
                .FirstOrDefaultAsync(m => m.ResourceId == id);
            if (learningResource == null)
            {
                return NotFound();
            }

            return View(learningResource);
        }

        // POST: LearningResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningResource = await _context.LearningResources.FindAsync(id);
            if (learningResource != null)
            {
                _context.LearningResources.Remove(learningResource);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningResourceExists(int id)
        {
            return _context.LearningResources.Any(e => e.ResourceId == id);
        }
    }
}
