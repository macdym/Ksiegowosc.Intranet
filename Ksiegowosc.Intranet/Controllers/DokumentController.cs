using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ksiegowosc.Data;
using Ksiegowosc.Data.Data;
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
            var dokumentyDto =await _service.GetAll(page, pagingInfo);
            model.Dokumenty = dokumentyDto;

            return View(model);
        }
        // GET: Kontrachent/CreateOrEdit
        public IActionResult Create()
        {
            return PartialView();
        }
    }
}
