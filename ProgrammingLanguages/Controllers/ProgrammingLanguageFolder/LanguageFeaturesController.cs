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
    public class LanguageFeaturesController : Controller
    {
        private readonly LanguageProjectContext _context;

        public LanguageFeaturesController(LanguageProjectContext context)
        {
            _context = context;
        }

        // GET: LanguageFeatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.LanguageFeatures.ToListAsync());
        }

        // GET: LanguageFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageFeature = await _context.LanguageFeatures
                .FirstOrDefaultAsync(m => m.FeatureId == id);
            if (languageFeature == null)
            {
                return NotFound();
            }

            return View(languageFeature);
        }

        // GET: LanguageFeatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LanguageFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeatureId,FeatureName,Description,F1,F2,F3")] LanguageFeature languageFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(languageFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(languageFeature);
        }

        // GET: LanguageFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageFeature = await _context.LanguageFeatures.FindAsync(id);
            if (languageFeature == null)
            {
                return NotFound();
            }
            return View(languageFeature);
        }

        // POST: LanguageFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeatureId,FeatureName,Description,F1,F2,F3")] LanguageFeature languageFeature)
        {
            if (id != languageFeature.FeatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(languageFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageFeatureExists(languageFeature.FeatureId))
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
            return View(languageFeature);
        }

        // GET: LanguageFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageFeature = await _context.LanguageFeatures
                .FirstOrDefaultAsync(m => m.FeatureId == id);
            if (languageFeature == null)
            {
                return NotFound();
            }

            return View(languageFeature);
        }

        // POST: LanguageFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var languageFeature = await _context.LanguageFeatures.FindAsync(id);
            if (languageFeature != null)
            {
                _context.LanguageFeatures.Remove(languageFeature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageFeatureExists(int id)
        {
            return _context.LanguageFeatures.Any(e => e.FeatureId == id);
        }
    }
}
