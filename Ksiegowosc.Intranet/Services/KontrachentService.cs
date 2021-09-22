using AutoMapper;
using Ksiegowosc.Data;
using Ksiegowosc.Data.Data;
using Ksiegowosc.Intranet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Ksiegowosc.Intranet.Services
{
    public interface IKontrachentService
    {
        Task<IPagedList<KontrachentDto>> GetKontrachenci(int? page);
    }

    public class KontrachentService : IKontrachentService
    {
        private readonly KsiegowoscDbContext _dbContext;
        private readonly IMapper _mapper;

        public KontrachentService(KsiegowoscDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IPagedList<KontrachentDto>> GetKontrachenci(int? page)
        {
            var kontrachenci =await _dbContext
                .Kontrachenci
                .Include(k => k.Adres)
                .ToListAsync();

            var kontrachenciDto = _mapper.Map<List<KontrachentDto>>(kontrachenci);

            return await kontrachenciDto.ToPagedListAsync(page?? 1,10);
        }
        //private async Task<PagedList> SortPaginateFiltrate(PageViewModel pageViewModel, ContractorSearchModel searchModel)
        //{
        //    ContractorBusinessLogic contractorBusinessLogic = new ContractorBusinessLogic(this._context);

        //    ViewBag.CurrentSort = pageViewModel.SortOrder;
        //    ViewBag.NameSortParm = pageViewModel.SortOrder == "Name" ? "name_desc" : "Name";
        //    ViewBag.VatSortParm = pageViewModel.SortOrder == "Vat" ? "vat_desc" : "Vat";
        //    ViewBag.OdbiorcaSortParm = pageViewModel.SortOrder == "Odbiorca" ? "odbiorca_desc" : "Odbiorca";
        //    ViewBag.DostawcaSortParm = pageViewModel.SortOrder == "Dostawca" ? "dostawca_desc" : "Dostawca";
        //    ViewBag.ZależnySortParm = pageViewModel.SortOrder == "Zależny" ? "zalezny_desc" : "Zależny";
        //    ViewBag.BankSortParm = pageViewModel.SortOrder == "Bank" ? "bank_desc" : "Bank";
        //    ViewBag.MiastoSortParm = pageViewModel.SortOrder == "Miasto" ? "miasto_desc" : "Miasto";

        //    if (pageViewModel.SearchString != null)
        //    {
        //        pageViewModel.Page = 1;
        //    }
        //    else
        //    {
        //        pageViewModel.SearchString = pageViewModel.CurrentFilter;
        //    }

        //    ViewBag.CurrentFilter = pageViewModel.SearchString;

        //    var contractors = contractorBusinessLogic.GetContractors(searchModel);

        //    if (!String.IsNullOrEmpty(pageViewModel.SearchString))
        //    {
        //        contractors = contractors
        //            .Where(c => c.FullName.ToLower().Contains(pageViewModel.SearchString.ToLower())
        //                || c.NipOrPesel.Contains(pageViewModel.SearchString)
        //                || c.City.ToLower().Contains(pageViewModel.SearchString.ToLower())
        //                || c.Bank.ToLower().Contains(pageViewModel.SearchString.ToLower()));
        //    }

        //    switch (pageViewModel.SortOrder)
        //    {
        //        case "name_desc":
        //            contractors = contractors.OrderByDescending(s => s.FullName);
        //            break;
        //        case "Name":
        //            contractors = contractors.OrderBy(s => s.FullName);
        //            break;
        //        case "vat_desc":
        //            contractors = contractors.OrderByDescending(s => s.VATpayer);
        //            break;
        //        case "Vat":
        //            contractors = contractors.OrderBy(s => s.VATpayer);
        //            break;
        //        case "odbiorca_desc":
        //            contractors = contractors.OrderByDescending(s => s.Recipient);
        //            break;
        //        case "Odbiorca":
        //            contractors = contractors.OrderBy(s => s.Recipient);
        //            break;
        //        case "dostawca_desc":
        //            contractors = contractors.OrderByDescending(s => s.Provider);
        //            break;
        //        case "Dostawca":
        //            contractors = contractors.OrderBy(s => s.Provider);
        //            break;
        //        case "zalezny_desc":
        //            contractors = contractors.OrderByDescending(s => s.Dependent);
        //            break;
        //        case "Zalezny":
        //            contractors = contractors.OrderBy(s => s.Dependent);
        //            break;
        //        case "bank_desc":
        //            contractors = contractors.OrderByDescending(s => s.Bank);
        //            break;
        //        case "Bank":
        //            contractors = contractors.OrderBy(s => s.Bank);
        //            break;
        //        case "miasto_desc":
        //            contractors = contractors.OrderByDescending(s => s.City);
        //            break;
        //        case "Miasto":
        //            contractors = contractors.OrderBy(s => s.City);
        //            break;
        //        default:
        //            contractors = contractors.OrderByDescending(s => s.ContractorId);
        //            break;
        //    }
        //    if (pageViewModel.PageSize == 0)
        //    {
        //        pageViewModel.PageSize = 10;
        //    }
        //    int pageNumber = (pageViewModel.Page ?? 1);

        //    return await contractors.ToPagedListAsync(pageNumber, pageViewModel.PageSize);
        //}
    }
}
