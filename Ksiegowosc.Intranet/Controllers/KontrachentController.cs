using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ksiegowosc.Intranet.Services;
using Ksiegowosc.Intranet.ViewModels;
using Ksiegowosc.Intranet.Models;
using Microsoft.AspNetCore.Authorization;
using Ksiegowosc.Data.Data;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
            var kontrachenciDto = _service.GetAll(page,pagingInfo,filtry);
            model.Kontrachenci = await kontrachenciDto;

            return View(model);
        }
        // GET: Kontrachent/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }
        // POST: Kontrachent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NipLubPesel,Regon,PlatnikVat,Nazwa,SkrotNazwy,Dostawca,Odbiorca,Zalezny,Bank,NumerKonta,Ulica,Miasto,KodPocztowy")] KontrachentDto KontrachentDto)
        {
            await _service.Create(KontrachentDto);

            return RedirectToAction(nameof(Index));
        }
        
        // POST: Kontrachent/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Kontrachent/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = new KontrachentViewModel();
            var kontrachentDto =await _service.GetKontrachentDto(id);
            model.KontrachentDto = kontrachentDto;

            return PartialView(model);
        }
        // GET: Kontrachent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new KontrachentViewModel();
            var kontrachentDto = await _service.GetKontrachentDto(id);
            model.KontrachentDto = kontrachentDto;

            return PartialView(model);
        }
        
        // POST: Kontrachent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,[Bind("IdKontrachenta,NipLubPesel, Regon, PlatnikVat, Nazwa, SkrotNazwy, Dostawca, Odbiorca, Zalezny, Bank, NumerKonta, Ulica, Miasto, KodPocztowy")] KontrachentDto KontrachentDto)
        {
            await _service.Update(KontrachentDto);

            return RedirectToAction(nameof(Index));
        }
    }
}


//// GET: Kontrachent/CreateOrEdit
//public async Task<IActionResult> CreateOrEdit(int? id)
//{
//    if (id is null)
//    {
//        return PartialView();
//    }
//    var model = new KontrachentViewModel();
//    var kontrachentDto = await _service.GetKontrachentDto(id);
//    model.KontrachentDto = kontrachentDto;

//    return PartialView(model);
//}

//// POST: Kontrachent/CreateOrEdit/5?
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> CreateOrEdit(int? id, [Bind("IdKontrachenta,NipLubPesel, Regon, PlatnikVat, Nazwa, SkrotNazwy, Dostawca, Odbiorca, Zalezny, Bank, NumerKonta, Ulica, Miasto, KodPocztowy")] KontrachentDto KontrachentDto)
//{
//    if (id is null)
//    {
//        await _service.Create(KontrachentDto);
//    }
//    else
//    {
//        await _service.Update(KontrachentDto);
//    }

//    return RedirectToAction(nameof(Index));
//}