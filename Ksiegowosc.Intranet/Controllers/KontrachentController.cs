using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ksiegowosc.Intranet.Services;
using Ksiegowosc.Intranet.ViewModels;
using Ksiegowosc.Intranet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index(int? page, PagingInfo pagingInfo, FiltryKontrachentaDto filtry)
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
            var kontrachenciDto = await _service.GetAll(page,pagingInfo,filtry);
            model.Kontrachenci = kontrachenciDto;

            return View(model);
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
        // GET: Kontrachent/CreateOrEdit
        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            if (id is null)
            {
                return PartialView();
            }
            var model = new KontrachentViewModel();
            var kontrachentDto = await _service.GetKontrachentDto(id);
            model.KontrachentDto = kontrachentDto;

            return PartialView(model);
        }
        // POST: Kontrachent/CreateOrEdit/5?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("IdKontrachenta,NipLubPesel, Regon, PlatnikVat, Nazwa, SkrotNazwy, Dostawca, Odbiorca, Zalezny, Bank, NumerKonta, Ulica, Miasto, KodPocztowy")] KontrachentDto KontrachentDto)
        {
            int? id = KontrachentDto.IdKontrachenta;
            if (id == null)
            {
                await _service.Create(KontrachentDto);
            }
            else
            {
                await _service.Update(KontrachentDto);
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Kontrachent/Documents
        public async Task<IActionResult> Documents(int? id,int? page, PagingInfo pagingInfo)
        {
            ViewBag.CurrentSort = pagingInfo.SortOrder;
            ViewBag.NameSortParm = pagingInfo.SortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CurrentFilter = pagingInfo.SearchString;
            var szablony = await _service.GetSzablony();
            ViewData["IdDokumentu"] = new SelectList(szablony, "IdDokumentu", "NazwaDokumentu");

            var model = new KontrachentViewModel();
            var dokumentyKontrachentaDto = await _service.GetDokumenty(page, pagingInfo);
            var kontrachentDto = await _service.GetKontrachentDto(id);
            model.DokumentyKontrachenta = dokumentyKontrachentaDto;
            model.KontrachentDto = kontrachentDto;

            return PartialView(model);
        }
        // POST: Kontrachent/AddDocument
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDocument(CreateDokumentDto CreateDokumentDto)
        {
            await _service.AddDokument(CreateDokumentDto);


        }
    }
}




