using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace efCoreApp.Controllers
{
    
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;

        public OgrenciController(DataContext context)
        {
            _context = context;
        }
        
        public IActionResult Create()
        {
        return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var ogrenci = await _context.Ogrenciler.
                                Include(o => o.KursKayitlari).
                                ThenInclude(o => o.Kurs)
                                .FirstOrDefaultAsync(o => o.OgrenciId == id);
            if(ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Ogrenci model)
        {
            if(id != model.OgrenciId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Ogrenciler.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(x => x.OgrenciId == model.OgrenciId))
                    {
                        return NotFound();
                    }else{

                    throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if(ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteX([FromForm]int? id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if(ogrenci == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}