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
    public class ProgrammingLanguageFeaturesController : Controller
    {
        private readonly LanguageProjectContext _context;

        public ProgrammingLanguageFeaturesController(LanguageProjectContext context)
        {
            _context = context;
        }

        // GET: ProgrammingLanguageFeatures
        public async Task<IActionResult> Index()
        {
            var languageProjectContext = _context.ProgrammingLanguageFeatures.Include(p => p.Feature).Include(p => p.Language);
            return View(await languageProjectContext.ToListAsync());
        }

        // GET: ProgrammingLanguageFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmingLanguageFeature = await _context.ProgrammingLanguageFeatures
                .Include(p => p.Feature)
                .Include(p => p.Language)
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (programmingLanguageFeature == null)
            {
                return NotFound();
            }

            return View(programmingLanguageFeature);
        }

        // GET: ProgrammingLanguageFeatures/Create
        public IActionResult Create()
        {
            ViewData["FeatureId"] = new SelectList(_context.LanguageFeatures, "FeatureId", "FeatureId");
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId");
            return View();
        }

        // POST: ProgrammingLanguageFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,FeatureId,Pf1,Pf2,Pf3")] ProgrammingLanguageFeature programmingLanguageFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programmingLanguageFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_context.LanguageFeatures, "FeatureId", "FeatureId", programmingLanguageFeature.FeatureId);
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", programmingLanguageFeature.LanguageId);
            return View(programmingLanguageFeature);
        }

        // GET: ProgrammingLanguageFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmingLanguageFeature = await _context.ProgrammingLanguageFeatures.FindAsync(id);
            if (programmingLanguageFeature == null)
            {
                return NotFound();
            }
            ViewData["FeatureId"] = new SelectList(_context.LanguageFeatures, "FeatureId", "FeatureId", programmingLanguageFeature.FeatureId);
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", programmingLanguageFeature.LanguageId);
            return View(programmingLanguageFeature);
        }

        // POST: ProgrammingLanguageFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanguageId,FeatureId,Pf1,Pf2,Pf3")] ProgrammingLanguageFeature programmingLanguageFeature)
        {
            if (id != programmingLanguageFeature.LanguageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programmingLanguageFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgrammingLanguageFeatureExists(programmingLanguageFeature.LanguageId))
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
            ViewData["FeatureId"] = new SelectList(_context.LanguageFeatures, "FeatureId", "FeatureId", programmingLanguageFeature.FeatureId);
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", programmingLanguageFeature.LanguageId);
            return View(programmingLanguageFeature);
        }

        // GET: ProgrammingLanguageFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmingLanguageFeature = await _context.ProgrammingLanguageFeatures
                .Include(p => p.Feature)
                .Include(p => p.Language)
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (programmingLanguageFeature == null)
            {
                return NotFound();
            }

            return View(programmingLanguageFeature);
        }

        // POST: ProgrammingLanguageFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programmingLanguageFeature = await _context.ProgrammingLanguageFeatures.FindAsync(id);
            if (programmingLanguageFeature != null)
            {
                _context.ProgrammingLanguageFeatures.Remove(programmingLanguageFeature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgrammingLanguageFeatureExists(int id)
        {
            return _context.ProgrammingLanguageFeatures.Any(e => e.LanguageId == id);
        }
    }
}
