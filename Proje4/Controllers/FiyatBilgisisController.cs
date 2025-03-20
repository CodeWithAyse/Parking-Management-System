using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje4.Models;

namespace Proje4.Controllers
{
    public class FiyatBilgisisController : Controller
    {
        private readonly OtoparkContext _context;

        public FiyatBilgisisController(OtoparkContext context)
        {
            _context = context;
        }

        // GET: FiyatBilgisis
        public async Task<IActionResult> Index()
        {
            var otoparkContext = _context.FiyatBilgisis.Include(f => f.Musteri);
            return View(await otoparkContext.ToListAsync());
        }

        // GET: FiyatBilgisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FiyatBilgisis == null)
            {
                return NotFound();
            }

            var fiyatBilgisi = await _context.FiyatBilgisis
                .Include(f => f.Musteri)
                .FirstOrDefaultAsync(m => m.FiyatId == id);
            if (fiyatBilgisi == null)
            {
                return NotFound();
            }

            return View(fiyatBilgisi);
        }

        // GET: FiyatBilgisis/Create
        public IActionResult Create()
        {
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriAd");
            return View();
        }

        // POST: FiyatBilgisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FiyatId,Fiyat,Saat,ToplamTutar,MusteriId")] FiyatBilgisi fiyatBilgisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fiyatBilgisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriAd", fiyatBilgisi.MusteriId);
            return View(fiyatBilgisi);
        }

        // GET: FiyatBilgisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FiyatBilgisis == null)
            {
                return NotFound();
            }

            var fiyatBilgisi = await _context.FiyatBilgisis.FindAsync(id);
            if (fiyatBilgisi == null)
            {
                return NotFound();
            }
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriAd", fiyatBilgisi.MusteriId);
            return View(fiyatBilgisi);
        }

        // POST: FiyatBilgisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FiyatId,Fiyat,Saat,ToplamTutar,MusteriId")] FiyatBilgisi fiyatBilgisi)
        {
            if (id != fiyatBilgisi.FiyatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fiyatBilgisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiyatBilgisiExists(fiyatBilgisi.FiyatId))
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
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriAd", fiyatBilgisi.MusteriId);
            return View(fiyatBilgisi);
        }

        // GET: FiyatBilgisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FiyatBilgisis == null)
            {
                return NotFound();
            }

            var fiyatBilgisi = await _context.FiyatBilgisis
                .Include(f => f.Musteri)
                .FirstOrDefaultAsync(m => m.FiyatId == id);
            if (fiyatBilgisi == null)
            {
                return NotFound();
            }

            return View(fiyatBilgisi);
        }

        // POST: FiyatBilgisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FiyatBilgisis == null)
            {
                return Problem("Entity set 'OtoparkContext.FiyatBilgisis'  is null.");
            }
            var fiyatBilgisi = await _context.FiyatBilgisis.FindAsync(id);
            if (fiyatBilgisi != null)
            {
                _context.FiyatBilgisis.Remove(fiyatBilgisi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FiyatBilgisiExists(int id)
        {
          return (_context.FiyatBilgisis?.Any(e => e.FiyatId == id)).GetValueOrDefault();
        }
    }
}
