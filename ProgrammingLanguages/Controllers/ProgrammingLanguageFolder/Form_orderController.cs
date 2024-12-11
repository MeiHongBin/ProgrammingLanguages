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
    public class Form_orderController : Controller
    {
        private readonly LanguageProjectContext _context;

        public Form_orderController(LanguageProjectContext context)
        {
            _context = context;
        }

        // GET: Form_order
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormOrders.ToListAsync());
        }

        // GET: Form_order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form_order = await _context.FormOrders
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (form_order == null)
            {
                return NotFound();
            }

            return View(form_order);
        }

        // GET: Form_order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Form_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormId,FormName,FormOrder")] Form_order form_order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(form_order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form_order);
        }

        // GET: Form_order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form_order = await _context.FormOrders.FindAsync(id);
            if (form_order == null)
            {
                return NotFound();
            }
            return View(form_order);
        }

        // POST: Form_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormId,FormName,FormOrder")] Form_order form_order)
        {
            if (id != form_order.FormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(form_order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Form_orderExists(form_order.FormId))
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
            return View(form_order);
        }

        // GET: Form_order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form_order = await _context.FormOrders
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (form_order == null)
            {
                return NotFound();
            }

            return View(form_order);
        }

        // POST: Form_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var form_order = await _context.FormOrders.FindAsync(id);
            if (form_order != null)
            {
                _context.FormOrders.Remove(form_order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Form_orderExists(int id)
        {
            return _context.FormOrders.Any(e => e.FormId == id);
        }
    }
}
