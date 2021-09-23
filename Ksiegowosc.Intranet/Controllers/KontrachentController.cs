using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ksiegowosc.Intranet.Services;
using Ksiegowosc.Intranet.ViewModels;
using Ksiegowosc.Intranet.Models;
using Microsoft.AspNetCore.Authorization;
using Ksiegowosc.Data.Data;

namespace Ksiegowosc.Intranet.Controllers
{
    [Authorize]
    public class KontrachentController : Controller
    {
        private readonly IKontrachentService _service;

        public KontrachentController(IKontrachentService service)
        {
            _service = service;
        }

        // GET: Kontrachent
        public async Task<IActionResult> Index(int? page, KontrachentPagingInfo pagingInfo, FiltryKontrachentaDto filtry)
        {
            ViewBag.CurrentSort = pagingInfo.SortOrder;
            ViewBag.NameSortParm = pagingInfo.SortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.VatSortParm = pagingInfo.SortOrder == "Vat" ? "vat_desc" : "Vat";
            ViewBag.OdbiorcaSortParm = pagingInfo.SortOrder == "Odbiorca" ? "odbiorca_desc" : "Odbiorca";
            ViewBag.DostawcaSortParm = pagingInfo.SortOrder == "Dostawca" ? "dostawca_desc" : "Dostawca";
            ViewBag.ZaleznySortParm = pagingInfo.SortOrder == "Zalezny" ? "zalezny_desc" : "Zalezny";
            ViewBag.BankSortParm = pagingInfo.SortOrder == "Bank" ? "bank_desc" : "Bank";
            ViewBag.MiastoSortParm = pagingInfo.SortOrder == "Miasto" ? "miasto_desc" : "Miasto";

            ViewBag.CurrentFilter = pagingInfo.SearchString;

            var model = new KontrachentViewModel();
            var kontrachenciDto = _service.GetKontrachenci(page,pagingInfo,filtry);
            model.Kontrachenci = await kontrachenciDto;

            return View(model);
        }
        // GET: Kontrachent/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Kontrachent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NipLubPesel,Regon,PlatnikVat,Nazwa,SkrotNazwy,Dostawca,Odbiorca,Zalezny,Bank,NumerKonta,Ulica,Miasto,KodPocztowy")] CreateKontrachentDto dto)
        {
            var model = new KontrachentViewModel();
            model.Kontrachenci = await _service.Create(dto);

            return View("Index",model);
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
