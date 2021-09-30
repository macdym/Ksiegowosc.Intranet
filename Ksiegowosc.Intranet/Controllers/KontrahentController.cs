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
    public class KontrahentController : Controller
    {
        private readonly IKontrahentService _service;

        public KontrahentController(IKontrahentService service)
        {
            _service = service;
        }

        // GET: Kontrahent
        public async Task<IActionResult> Index(int? page, PagingInfo pagingInfo, FiltryKontrahentaDto filtry)
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

            var szablony = await _service.GetSzablony();
            ViewData["IdDokumentu"] = new SelectList(szablony, "IdDokumentu", "NazwaDokumentu");

            var model = new KontrahentViewModel();
            var kontrahenciDto = await _service.GetAll(page, pagingInfo, filtry);
            model.Kontrahenci = kontrahenciDto;

            return View(model);
        }
        // POST: Kontrahent/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Kontrahent/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = new KontrahentViewModel();
            var kontrahentDto = await _service.GetKontrahentDto(id);
            model.KontrahentDto = kontrahentDto;

            return PartialView(model);
        }
        // GET: Kontrahent/CreateOrEdit
        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            if (id is null)
            {
                return PartialView();
            }
            var model = new KontrahentViewModel();
            var kontrahentDto = await _service.GetKontrahentDto(id);
            model.KontrahentDto = kontrahentDto;

            return PartialView(model);
        }
        // POST: Kontrahent/CreateOrEdit/5?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("IdKontrahenta,NipLubPesel, Regon, PlatnikVat, Nazwa, SkrotNazwy, Dostawca, Odbiorca, Zalezny, Bank, NumerKonta, Ulica, Miasto, KodPocztowy")] KontrahentDto KontrahentDto)
        {
            int? id = KontrahentDto.IdKontrahenta;
            if (id == null)
            {
                await _service.Create(KontrahentDto);
            }
            else
            {
                await _service.Update(KontrahentDto);
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Kontrahent/Dokumenty
        public async Task<IActionResult> Dokumenty(int id, int? page, PagingInfo pagingInfo)
        {
            ViewBag.CurrentSort = pagingInfo.SortOrder;
            ViewBag.NameSortParm = pagingInfo.SortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CurrentFilter = pagingInfo.SearchString;
            var szablony = await _service.GetSzablony();
            ViewData["IdDokumentu"] = new SelectList(szablony, "IdDokumentu", "NazwaDokumentu");

            var model = new KontrahentViewModel();
            var dokumentyKontrahentaDto = await _service.GetDokumenty(id,page, pagingInfo);
            var kontrahentDto = await _service.GetKontrahentDto(id);
            model.DokumentyKontrahenta = dokumentyKontrahentaDto;
            model.KontrahentDto = kontrahentDto;

            return PartialView(model);
        }
        // POST: Kontrahent/AddDokument
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDokument(int id,DokumentKontrahentaDto DokumentKontrahentaDto)
        {
            DokumentKontrahentaDto.IdKontrahenta = id;
            var fileDto = await _service.AddDokument(DokumentKontrahentaDto);
            return File(fileDto.fileBytes, "application/doc", $"{fileDto.fileName}.doc");
        }
        //POST: Dokument/EditDokument/5
        public async Task EditDokument(int? id)
        {
            await _service.EditDokument(id);
        }
        // POST: Dokument/DeleteDokument/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDokument(int? id)
        {
            await _service.DeleteDokument(id);
            return RedirectToAction(nameof(Index));
        }
        // GET: Kontrahent/DownloadDokument
        public async Task<IActionResult> DownloadDokument(int id)
        {
            var fileDto = await _service.DownloadDokument(id);
            return File(fileDto.fileBytes, "application/doc", $"{fileDto.fileName}.doc");
        }
        // GET: Kontrahent/AddAndDownloadDocumentyDoc
        public async Task<IActionResult> DownloadDokumentyDoc(int[] ids, DokumentKontrahentaDto DokumentKontrahentaDto)
        {
            var fileDto =await _service.DownloadDokumentyDoc(ids, DokumentKontrahentaDto);
            return File(fileDto.fileBytes, "application/octet-stream", $"Kontrachenci_Doc.zip");
        }
        // GET: Kontrahent/AddAndDownloadDocumentyZip
        public async Task<IActionResult> DownloadDokumentyZip(int[] ids, DokumentKontrahentaDto DokumentKontrahentaDto)
        {
            var zipStream = await _service.DownloadDokumentyZip(ids, DokumentKontrahentaDto);
            return File(zipStream, "application/octet-stream", $"Kontrachenci_ZIP.zip");
        }
    }
}




