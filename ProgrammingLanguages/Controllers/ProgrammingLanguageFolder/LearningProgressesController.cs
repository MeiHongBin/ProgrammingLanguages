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
    public class LearningProgressesController : Controller
    {
        private readonly LanguageProjectContext _context;

        public LearningProgressesController(LanguageProjectContext context)
        {
            _context = context;
        }

        // GET: LearningProgresses
        public async Task<IActionResult> Index()
        {
            var languageProjectContext = _context.LearningProgresses.Include(l => l.Language).Include(l => l.User);
            return View(await languageProjectContext.ToListAsync());
        }

        // GET: LearningProgresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningProgress = await _context.LearningProgresses
                .Include(l => l.Language)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ProgressId == id);
            if (learningProgress == null)
            {
                return NotFound();
            }

            return View(learningProgress);
        }

        // GET: LearningProgresses/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: LearningProgresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgressId,UserId,LanguageId,ProficiencyLevel,LearningHours,Certification,LastUpdated,Lp1,Lp2,Lp3")] LearningProgress learningProgress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learningProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", learningProgress.LanguageId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", learningProgress.UserId);
            return View(learningProgress);
        }

        // GET: LearningProgresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningProgress = await _context.LearningProgresses.FindAsync(id);
            if (learningProgress == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", learningProgress.LanguageId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", learningProgress.UserId);
            return View(learningProgress);
        }

        // POST: LearningProgresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgressId,UserId,LanguageId,ProficiencyLevel,LearningHours,Certification,LastUpdated,Lp1,Lp2,Lp3")] LearningProgress learningProgress)
        {
            if (id != learningProgress.ProgressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningProgressExists(learningProgress.ProgressId))
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
            ViewData["LanguageId"] = new SelectList(_context.ProgrammingLanguages, "LanguageId", "LanguageId", learningProgress.LanguageId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", learningProgress.UserId);
            return View(learningProgress);
        }

        // GET: LearningProgresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningProgress = await _context.LearningProgresses
                .Include(l => l.Language)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ProgressId == id);
            if (learningProgress == null)
            {
                return NotFound();
            }

            return View(learningProgress);
        }

        // POST: LearningProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningProgress = await _context.LearningProgresses.FindAsync(id);
            if (learningProgress != null)
            {
                _context.LearningProgresses.Remove(learningProgress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningProgressExists(int id)
        {
            return _context.LearningProgresses.Any(e => e.ProgressId == id);
        }
    }
}
