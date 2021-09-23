using AutoMapper;
using Ksiegowosc.Data;
using Ksiegowosc.Intranet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Mvc;

namespace Ksiegowosc.Intranet.Services
{
    public interface IKontrachentService
    {
        Task<IPagedList<KontrachentDto>> GetKontrachenci(int? page, KontrachentPagingInfo pagingInfo, FiltryKontrachentaDto filtry);
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
        public async Task<IPagedList<KontrachentDto>> GetKontrachenci(int? page, KontrachentPagingInfo pagingInfo,FiltryKontrachentaDto filtry)
        {
            var kontrachenci =await _dbContext
                .Kontrachenci
                .Include(k => k.Adres)
                .ToListAsync();

            if (filtry != null)
            {
                if (filtry.Rodzaj != null)
                {
                    if (filtry.Rodzaj == "dostawca")
                    {
                        kontrachenci = await kontrachenci.Where(k => k.Dostawca.Equals(true)).ToListAsync();
                    }
                    if (filtry.Rodzaj == "odbiorca")
                    {
                        kontrachenci =await kontrachenci.Where(k => k.Odbiorca.Equals(true)).ToListAsync();
                    }
                }
                if (filtry.Zalezny != null)
                {
                    kontrachenci =await kontrachenci.Where(k => k.Zalezny.Equals(bool.Parse(filtry.Zalezny))).ToListAsync();
                }
                if (filtry.PlatnikVat != null)
                {
                    kontrachenci = await kontrachenci.Where(k => k.PlatnikVat.Equals(bool.Parse(filtry.PlatnikVat))).ToListAsync();
                }
            }

            if (pagingInfo.SearchString != null)
            {
                pagingInfo.Page = 1;
            }
            else
            {
                pagingInfo.SearchString = pagingInfo.CurrentFilter;
            }

            if (!String.IsNullOrEmpty(pagingInfo.SearchString))
            {
                kontrachenci = kontrachenci
                    .Where(k => k.Nazwa.ToLower().Contains(pagingInfo.SearchString.ToLower())).ToList();
            }

            switch (pagingInfo.SortOrder)
            {
                case "name_desc":
                    kontrachenci = kontrachenci.OrderByDescending(k=>k.Nazwa).ToList();
                    break;
                case "Name":
                    kontrachenci = kontrachenci.OrderBy(k => k.Nazwa).ToList();
                    break;
                case "vat_desc":
                    kontrachenci = kontrachenci.OrderByDescending(k => k.PlatnikVat).ToList();
                    break;
                case "Vat":
                    kontrachenci = kontrachenci.OrderBy(k => k.PlatnikVat).ToList();
                    break;
                case "odbiorca_desc":
                    kontrachenci = kontrachenci.OrderByDescending(k => k.Odbiorca).ToList();
                    break;
                case "Odbiorca":
                    kontrachenci = kontrachenci.OrderBy(k => k.Odbiorca).ToList();
                    break;
                case "dostawca_desc":
                    kontrachenci = kontrachenci.OrderByDescending(k => k.Dostawca).ToList();
                    break;
                case "Dostawca":
                    kontrachenci = kontrachenci.OrderBy(k => k.Dostawca).ToList();
                    break;
                case "zalezny_desc":
                    kontrachenci = kontrachenci.OrderByDescending(k => k.Zalezny).ToList();
                    break;
                case "Zalezny":
                    kontrachenci = kontrachenci.OrderBy(k => k.Zalezny).ToList();
                    break;
                case "bank_desc":
                    kontrachenci = kontrachenci.OrderByDescending(k => k.Bank).ToList();
                    break;
                case "Bank":
                    kontrachenci = kontrachenci.OrderBy(k => k.Bank).ToList();
                    break;
                default:
                    kontrachenci = kontrachenci.OrderByDescending(k => k.IdKontrachenta).ToList();
                    break;
            }
            if (pagingInfo.PageSize == 0)
            {
                pagingInfo.PageSize = 10;
            }

            var kontrachenciDto = _mapper.Map<List<KontrachentDto>>(kontrachenci);

            return await kontrachenciDto.ToPagedListAsync(page?? 1,10);
        }
    }
}
