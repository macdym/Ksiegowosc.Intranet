using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ksiegowosc.Intranet.Models;
using Ksiegowosc.Intranet.Services;
using Ksiegowosc.Intranet.ViewModels;

namespace Ksiegowosc.Intranet.Controllers
{
    public class DokumentController : Controller
    {
        private readonly IDokumentService _service;

        public DokumentController(IDokumentService service)
        {
            _service = service;
        }

        // GET: Dokument
        public async Task<IActionResult> Index(int? page, PagingInfo pagingInfo)
        {
            ViewBag.CurrentSort = pagingInfo.SortOrder;
            ViewBag.NameSortParm = pagingInfo.SortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CurrentFilter = pagingInfo.SearchString;

            var model = new DokumentViewModel();
            var dokumentyDto = await _service.GetAll(page, pagingInfo);
            model.Dokumenty = dokumentyDto;

            return View(model);
        }
        // GET: Dokument/Create
        public IActionResult Create()
        {
            return PartialView();
        }
        // POST: Dokument/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDokumentDto CreateDokumentDto)
        {
            await _service.Create(CreateDokumentDto);
            return RedirectToAction(nameof(Index));
        }
        //POST: Dokument/Edit/5
        public async Task Edit(int? id)
        {
            await _service.Edit(id);
        }
        // POST: Dokument/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        // GET: Dokument/DownloadDokument
        public async Task<IActionResult> DownloadDokument(int? id)
        {
            var fileDto = await _service.DownloadDokument(id);
            return File(fileDto.fileBytes, "application/doc", $"{fileDto.fileName}.doc");
        }
    }
}
