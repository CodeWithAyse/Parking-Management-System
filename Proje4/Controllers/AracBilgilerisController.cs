using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proje4.Models;
using Microsoft.AspNetCore.Hosting; 
using System.IO;

namespace Proje4.Controllers
{
    public class AracBilgilerisController : Controller
    {
        private readonly OtoparkContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AracBilgilerisController(OtoparkContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: AracBilgileris
        public async Task<IActionResult> Index()
        {
            var otoparkContext = _context.AracBilgileris.Include(a => a.Musteri);
            return View(await otoparkContext.ToListAsync());
        }

        // GET: AracBilgileris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AracBilgileris == null)
            {
                return NotFound();
            }

            var aracBilgileri = await _context.AracBilgileris
                .Include(a => a.Musteri)
                .FirstOrDefaultAsync(m => m.AracId == id);
            if (aracBilgileri == null)
            {
                return NotFound();
            }

            return View(aracBilgileri);
        }

        // GET: AracBilgileris/Create
        public IActionResult Create()
        {
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriAd");
            return View();
        }

        // POST: AracBilgileris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AracId,AracPlaka,AracMarka,AracModel,MusteriId,ImageFile")] AracBilgileri aracBilgileri)
        {
            //if (ModelState.IsValid)
            //{
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(aracBilgileri.ImageFile.FileName);
                string extension = Path.GetExtension(aracBilgileri.ImageFile.FileName);
                aracBilgileri.AracPhoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwrootpath + "/Image/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                  await aracBilgileri.ImageFile.CopyToAsync(filestream);
                }
                _context.Add(aracBilgileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriId", aracBilgileri.MusteriId);
            return View(aracBilgileri);
        }

        // GET: AracBilgileris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AracBilgileris == null)
            {
                return NotFound();
            }

            var aracBilgileri = await _context.AracBilgileris.FindAsync(id);
            if (aracBilgileri == null)
            {
                return NotFound();
            }
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriAd", aracBilgileri.MusteriId);
            return View(aracBilgileri);
        }

        // POST: AracBilgileris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AracId,AracPlaka,AracMarka,AracModel,MusteriId,ImageFile")] AracBilgileri aracBilgileri)
        {
            if (id != aracBilgileri.AracId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aracBilgileri.ImageFile != null)
                    {
                        string wwwrootpath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(aracBilgileri.ImageFile.FileName);
                        string extension = Path.GetExtension(aracBilgileri.ImageFile.FileName);
                        aracBilgileri.AracPhoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwrootpath, "Image", fileName);

                        // Eski resmi sil
                        var existingImagePath = Path.Combine(wwwrootpath, "Image", aracBilgileri.AracPhoto);
                        if (System.IO.File.Exists(existingImagePath))
                        {
                            System.IO.File.Delete(existingImagePath);
                        }

                        // Yeni resmi kaydet
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await aracBilgileri.ImageFile.CopyToAsync(filestream);
                        }
                    }

                    _context.Update(aracBilgileri);
                    await _context.SaveChangesAsync();
                }
                //try
                //{
                //    _context.Update(aracBilgileri);
                //    await _context.SaveChangesAsync();
                //}
                catch (DbUpdateConcurrencyException)
                {
                    if (!AracBilgileriExists(aracBilgileri.AracId))
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
            ViewData["MusteriId"] = new SelectList(_context.MusteriBilgileris, "MusteriId", "MusteriAd", aracBilgileri.MusteriId);
            return View(aracBilgileri);
        }

        // GET: AracBilgileris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AracBilgileris == null)
            {
                return NotFound();
            }

            var aracBilgileri = await _context.AracBilgileris
                .Include(a => a.Musteri)
                .FirstOrDefaultAsync(m => m.AracId == id);
            if (aracBilgileri == null)
            {
                return NotFound();
            }

            return View(aracBilgileri);
        }

        // POST: AracBilgileris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AracBilgileris == null)
            {
                return Problem("Entity set 'OtoparkContext.AracBilgileris'  is null.");
            }
            var aracBilgileri = await _context.AracBilgileris.FindAsync(id);
            if (aracBilgileri != null)
            {
                _context.AracBilgileris.Remove(aracBilgileri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AracBilgileriExists(int id)
        {
          return (_context.AracBilgileris?.Any(e => e.AracId == id)).GetValueOrDefault();
        }
    }
}
