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
    public class MusteriBilgilerisController : Controller
    {
        private readonly OtoparkContext _context;

        public MusteriBilgilerisController(OtoparkContext context)
        {
            _context = context;
        }

        // GET: MusteriBilgileris
        public async Task<IActionResult> Index()
        {
            var otoparkContext = _context.MusteriBilgileris.Include(m => m.Park);
            return View(await otoparkContext.ToListAsync());
        }

        // GET: MusteriBilgileris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MusteriBilgileris == null)
            {
                return NotFound();
            }

            var musteriBilgileri = await _context.MusteriBilgileris
                .Include(m => m.Park)
                .FirstOrDefaultAsync(m => m.MusteriId == id);
            if (musteriBilgileri == null)
            {
                return NotFound();
            }

            return View(musteriBilgileri);
        }

        // GET: MusteriBilgileris/Create
        public IActionResult Create()
        {
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd");
            return View();
        }

        // POST: MusteriBilgileris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MusteriId,MusteriAd,MusteriSoyad,MusteriTc,MusteriTel,ParkId")] MusteriBilgileri musteriBilgileri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musteriBilgileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd", musteriBilgileri.ParkId);
            return View(musteriBilgileri);
        }

        // GET: MusteriBilgileris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MusteriBilgileris == null)
            {
                return NotFound();
            }

            var musteriBilgileri = await _context.MusteriBilgileris.FindAsync(id);
            if (musteriBilgileri == null)
            {
                return NotFound();
            }
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd", musteriBilgileri.ParkId);
            return View(musteriBilgileri);
        }

        // POST: MusteriBilgileris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MusteriId,MusteriAd,MusteriSoyad,MusteriTc,MusteriTel,ParkId")] MusteriBilgileri musteriBilgileri)
        {
            if (id != musteriBilgileri.MusteriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musteriBilgileri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusteriBilgileriExists(musteriBilgileri.MusteriId))
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
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd", musteriBilgileri.ParkId);
            return View(musteriBilgileri);
        }

        // GET: MusteriBilgileris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MusteriBilgileris == null)
            {
                return NotFound();
            }

            var musteriBilgileri = await _context.MusteriBilgileris
                .Include(m => m.Park)
                .FirstOrDefaultAsync(m => m.MusteriId == id);
            if (musteriBilgileri == null)
            {
                return NotFound();
            }

            return View(musteriBilgileri);
        }

        // POST: MusteriBilgileris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MusteriBilgileris == null)
            {
                return Problem("Entity set 'OtoparkContext.MusteriBilgileris'  is null.");
            }
            var musteriBilgileri = await _context.MusteriBilgileris.FindAsync(id);
            if (musteriBilgileri != null)
            {
                _context.MusteriBilgileris.Remove(musteriBilgileri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusteriBilgileriExists(int id)
        {
          return (_context.MusteriBilgileris?.Any(e => e.MusteriId == id)).GetValueOrDefault();
        }
    }
}
