using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ksiegowosc.Data;
using Ksiegowosc.Data.Data;
using Ksiegowosc.Intranet.Services;
using Ksiegowosc.Intranet.ViewModels;
using Ksiegowosc.Intranet.Models;
using X.PagedList;

namespace Ksiegowosc.Intranet.Controllers
{
    public class KontrachentController : Controller
    {
        private readonly IKontrachentService _service;

        public KontrachentController(IKontrachentService service)
        {
            _service = service;
        }

        // GET: Kontrachent
        public async Task<IActionResult> Index(int? page)
        {
            var model = new KontrachentViewModel();
            var kontrachenciDto = _service.GetKontrachenci(page);
            model.Kontrachenci = await kontrachenciDto;
            return View(model);
        }

        //// GET: Kontrachent/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var kontrachent = await _context.Kontrachenci
        //        .FirstOrDefaultAsync(m => m.IdKontrachenta == id);
        //    if (kontrachent == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(kontrachent);
        //}

        //// GET: Kontrachent/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Kontrachent/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdKontrachenta,NipLubPesel,Regon,PlatnikVat,Nazwa,SkrotNazwy,Dostawca,Odbiorca,Zalezny,Bank,NumerKonta,IdAdresu")] Kontrachent kontrachent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(kontrachent);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(kontrachent);
        //}

        //// GET: Kontrachent/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var kontrachent = await _context.Kontrachenci.FindAsync(id);
        //    if (kontrachent == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(kontrachent);
        //}

        //// POST: Kontrachent/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdKontrachenta,NipLubPesel,Regon,PlatnikVat,Nazwa,SkrotNazwy,Dostawca,Odbiorca,Zalezny,Bank,NumerKonta,IdAdresu")] Kontrachent kontrachent)
        //{
        //    if (id != kontrachent.IdKontrachenta)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(kontrachent);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!KontrachentExists(kontrachent.IdKontrachenta))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(kontrachent);
        //}

        //// GET: Kontrachent/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var kontrachent = await _context.Kontrachenci
        //        .FirstOrDefaultAsync(m => m.IdKontrachenta == id);
        //    if (kontrachent == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(kontrachent);
        //}

        //// POST: Kontrachent/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var kontrachent = await _context.Kontrachenci.FindAsync(id);
        //    _context.Kontrachenci.Remove(kontrachent);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool KontrachentExists(int id)
        //{
        //    return _context.Kontrachenci.Any(e => e.IdKontrachenta == id);
        //}
    }
}
