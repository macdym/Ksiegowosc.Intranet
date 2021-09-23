﻿using AutoMapper;
using Ksiegowosc.Data;
using Ksiegowosc.Intranet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Mvc;
using Ksiegowosc.Data.Data;

namespace Ksiegowosc.Intranet.Services
{
    public interface IKontrachentService
    {
        Task<IPagedList<KontrachentDto>> GetKontrachenci(int? page, KontrachentPagingInfo pagingInfo, FiltryKontrachentaDto filtry);
        Task<IPagedList<KontrachentDto>> Create(CreateKontrachentDto dto);
        Task Delete(int? id);
        Task<Kontrachent> GetKontrachent(int? id);
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
        #region Edit

        #endregion
        #region Delete
        public async Task Delete(int? id)
        {
            var kontrachent = await _dbContext
                .Kontrachenci
                .FirstOrDefaultAsync(k=>k.IdKontrachenta == id);

            _dbContext.Kontrachenci.Remove(kontrachent);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
        #region Create
        public async Task<IPagedList<KontrachentDto>> Create(CreateKontrachentDto dto)
        {
            var kontrachent = _mapper.Map<Kontrachent>(dto);
            await _dbContext.Kontrachenci.AddAsync(kontrachent);
            await _dbContext.SaveChangesAsync();

            var kontrachenci =await _dbContext
                .Kontrachenci
                .Include(k=>k.Adres)
                .ToListAsync();

            var kontrachenciDto = _mapper.Map<List<KontrachentDto>>(kontrachenci);

            return await kontrachenciDto.ToPagedListAsync(1, 10);
        }
        #endregion
        #region GetKontrachenciDto
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

            return await kontrachenciDto.ToPagedListAsync(page?? 1,pagingInfo.PageSize);
        }
        #endregion
        #region GetKontrachent
        public async Task<Kontrachent> GetKontrachent(int? id)
        {
            var kontrachent = await _dbContext
                .Kontrachenci
                .FirstOrDefaultAsync(k => k.IdKontrachenta == id);

            return kontrachent;
        }
        #endregion
    }
}
