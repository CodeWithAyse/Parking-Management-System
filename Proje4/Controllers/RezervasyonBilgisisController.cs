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
    public class RezervasyonBilgisisController : Controller
    {
        private readonly OtoparkContext _context;

        public RezervasyonBilgisisController(OtoparkContext context)
        {
            _context = context;
        }

        // GET: RezervasyonBilgisis
        public async Task<IActionResult> Index()
        {
            var otoparkContext = _context.RezervasyonBilgisis.Include(r => r.Arac).Include(r => r.Park);
            return View(await otoparkContext.ToListAsync());
        }

        // GET: RezervasyonBilgisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RezervasyonBilgisis == null)
            {
                return NotFound();
            }

            var rezervasyonBilgisi = await _context.RezervasyonBilgisis
                .Include(r => r.Arac)
                .Include(r => r.Park)
                .FirstOrDefaultAsync(m => m.RezervasyonId == id);
            if (rezervasyonBilgisi == null)
            {
                return NotFound();
            }

            return View(rezervasyonBilgisi);
        }

        // GET: RezervasyonBilgisis/Create
        public IActionResult Create()
        {
           ViewData["AracId"] = new SelectList(_context.AracBilgileris, "AracId", "AracPlaka");
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd");
            return View();
        }

        // POST: RezervasyonBilgisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezervasyonId,AracId,ParkId,StartTime,FinishTime")] RezervasyonBilgisi rezervasyonBilgisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervasyonBilgisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AracId"] = new SelectList(_context.AracBilgileris, "AracId", "AracPlaka", rezervasyonBilgisi.AracId);
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd", rezervasyonBilgisi.ParkId);
            return View(rezervasyonBilgisi);
        }

        // GET: RezervasyonBilgisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RezervasyonBilgisis == null)
            {
                return NotFound();
            }

            var rezervasyonBilgisi = await _context.RezervasyonBilgisis.FindAsync(id);
            if (rezervasyonBilgisi == null)
            {
                return NotFound();
            }
            ViewData["AracId"] = new SelectList(_context.AracBilgileris, "AracId", "AracPlaka", rezervasyonBilgisi.AracId);
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd", rezervasyonBilgisi.ParkId);
            return View(rezervasyonBilgisi);
        }

        // POST: RezervasyonBilgisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezervasyonId,AracId,ParkId,StartTime,FinishTime")] RezervasyonBilgisi rezervasyonBilgisi)
        {
            if (id != rezervasyonBilgisi.RezervasyonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervasyonBilgisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervasyonBilgisiExists(rezervasyonBilgisi.RezervasyonId))
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
            ViewData["AracId"] = new SelectList(_context.AracBilgileris, "AracId", "AracPlaka", rezervasyonBilgisi.AracId);
            ViewData["ParkId"] = new SelectList(_context.Parks, "ParkId", "ParkAd", rezervasyonBilgisi.ParkId);
            return View(rezervasyonBilgisi);
        }

        // GET: RezervasyonBilgisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RezervasyonBilgisis == null)
            {
                return NotFound();
            }

            var rezervasyonBilgisi = await _context.RezervasyonBilgisis
                .Include(r => r.Arac)
                .Include(r => r.Park)
                .FirstOrDefaultAsync(m => m.RezervasyonId == id);
            if (rezervasyonBilgisi == null)
            {
                return NotFound();
            }

            return View(rezervasyonBilgisi);
        }

        // POST: RezervasyonBilgisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RezervasyonBilgisis == null)
            {
                return Problem("Entity set 'OtoparkContext.RezervasyonBilgisis'  is null.");
            }
            var rezervasyonBilgisi = await _context.RezervasyonBilgisis.FindAsync(id);
            if (rezervasyonBilgisi != null)
            {
                _context.RezervasyonBilgisis.Remove(rezervasyonBilgisi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervasyonBilgisiExists(int id)
        {
          return (_context.RezervasyonBilgisis?.Any(e => e.RezervasyonId == id)).GetValueOrDefault();
        }
    }
}
