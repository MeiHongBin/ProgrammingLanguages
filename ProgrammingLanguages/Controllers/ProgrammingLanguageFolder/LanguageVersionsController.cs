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
    public class LanguageVersionsController : Controller
    {
        private readonly LanguageProjectContext _context;

        public LanguageVersionsController(LanguageProjectContext context)
        {
            _context = context;
        }

        // GET: LanguageVersions
        public async Task<IActionResult> Index()
        {
            var languageProjectContext = _context.LanguageVersions.Include(l => l.Language);
            return View(await languageProjectContext.ToListAsync());
        }

        // GET: LanguageVersions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageVersion = await _context.LanguageVersions
                .Include(l => l.Language)
                .FirstOrDefaultAsync(m => m.VersionId == id);
            if (languageVersion == null)
            {
                return NotFound();
            }

            return View(languageVersion);
        }

        // GET: LanguageVersions/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId");
            return View();
        }

        // POST: LanguageVersions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VersionId,LanguageId,VersionName,ReleaseDate,Changes,Lv1,Lv2,Lv3")] LanguageVersion languageVersion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(languageVersion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", languageVersion.LanguageId);
            return View(languageVersion);
        }

        // GET: LanguageVersions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageVersion = await _context.LanguageVersions.FindAsync(id);
            if (languageVersion == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", languageVersion.LanguageId);
            return View(languageVersion);
        }

        // POST: LanguageVersions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VersionId,LanguageId,VersionName,ReleaseDate,Changes,Lv1,Lv2,Lv3")] LanguageVersion languageVersion)
        {
            if (id != languageVersion.VersionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(languageVersion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageVersionExists(languageVersion.VersionId))
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
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", languageVersion.LanguageId);
            return View(languageVersion);
        }

        // GET: LanguageVersions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageVersion = await _context.LanguageVersions
                .Include(l => l.Language)
                .FirstOrDefaultAsync(m => m.VersionId == id);
            if (languageVersion == null)
            {
                return NotFound();
            }

            return View(languageVersion);
        }

        // POST: LanguageVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var languageVersion = await _context.LanguageVersions.FindAsync(id);
            if (languageVersion != null)
            {
                _context.LanguageVersions.Remove(languageVersion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageVersionExists(int id)
        {
            return _context.LanguageVersions.Any(e => e.VersionId == id);
        }
    }
}
