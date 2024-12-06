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
    public class ProgrammingLanguagesController : Controller
    {
        private readonly LanguageProjectContext _context;

        public ProgrammingLanguagesController(LanguageProjectContext context)
        {
            _context = context;
        }

        // GET: ProgrammingLanguages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProgrammingLanguages.ToListAsync());
        }

        // GET: ProgrammingLanguages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmingLanguage = await _context.ProgrammingLanguages
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (programmingLanguage == null)
            {
                return NotFound();
            }

            return View(programmingLanguage);
        }

        // GET: ProgrammingLanguages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgrammingLanguages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,LanguageName,CreatedYear,Creator,Paradigm,Description,OfficialWebsite,L1,L2,L3")] ProgrammingLanguage programmingLanguage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programmingLanguage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(programmingLanguage);
        }

        // GET: ProgrammingLanguages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmingLanguage = await _context.ProgrammingLanguages.FindAsync(id);
            if (programmingLanguage == null)
            {
                return NotFound();
            }
            return View(programmingLanguage);
        }

        // POST: ProgrammingLanguages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanguageId,LanguageName,CreatedYear,Creator,Paradigm,Description,OfficialWebsite,L1,L2,L3")] ProgrammingLanguage programmingLanguage)
        {
            if (id != programmingLanguage.LanguageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programmingLanguage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgrammingLanguageExists(programmingLanguage.LanguageId))
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
            return View(programmingLanguage);
        }

        // GET: ProgrammingLanguages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmingLanguage = await _context.ProgrammingLanguages
                .FirstOrDefaultAsync(m => m.LanguageId == id);
            if (programmingLanguage == null)
            {
                return NotFound();
            }

            return View(programmingLanguage);
        }

        // POST: ProgrammingLanguages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programmingLanguage = await _context.ProgrammingLanguages.FindAsync(id);
            if (programmingLanguage != null)
            {
                _context.ProgrammingLanguages.Remove(programmingLanguage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgrammingLanguageExists(int id)
        {
            return _context.ProgrammingLanguages.Any(e => e.LanguageId == id);
        }
    }
}
