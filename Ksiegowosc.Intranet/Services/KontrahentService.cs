﻿using AutoMapper;
using Ksiegowosc.Data;
using Ksiegowosc.Intranet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Ksiegowosc.Data.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics;
using Aspose.Words;
using Aspose.Words.Replacing;

namespace Ksiegowosc.Intranet.Services
{
    public interface IKontrahentService
    {
        Task<IPagedList<KontrahentDto>> GetAll(int? page, PagingInfo pagingInfo, FiltryKontrahentaDto filtry);
        Task<KontrahentDto> GetKontrahentDto(int? id);
        Task Create(KontrahentDto dto);
        Task Update(KontrahentDto dto);
        Task Delete(int? id);
        Task<IPagedList<DokumentKontrahentaDto>> GetDokumenty(int id ,int? page, PagingInfo pagingInfo);
        Task<IEnumerable<DokumentDto>> GetSzablony();
        Task<FileDto> AddDokument(DokumentKontrahentaDto dto);
        Task<FileDto> DownloadDokument(int? id);
    }

    public class KontrahentService : IKontrahentService
    {
        private readonly KsiegowoscDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KontrahentService(KsiegowoscDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        #region Delete
        public async Task Delete(int? id)
        {
            var kontrahent = await _dbContext
                .Kontrahenci
                .FirstOrDefaultAsync(k => k.IdKontrahenta == id);

            _dbContext.Kontrahenci.Remove(kontrahent);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
        #region Create
        public async Task Create(KontrahentDto dto)
        {
            var kontrahent = _mapper.Map<Kontrahent>(dto);
            await _dbContext.Kontrahenci.AddAsync(kontrahent);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
        #region Edit
        public async Task Update(KontrahentDto dto)
        {
            var kontrahentDto = _mapper.Map<Kontrahent>(dto);
            _dbContext.Update(kontrahentDto);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
        #region GetAll
        public async Task<IPagedList<KontrahentDto>> GetAll(int? page, PagingInfo pagingInfo, FiltryKontrahentaDto filtry)
        {
            var kontrahenci = await _dbContext
                .Kontrahenci
                .Include(k => k.Adres)
                .ToListAsync();

            if (filtry != null)
            {
                if (filtry.Rodzaj != null)
                {
                    if (filtry.Rodzaj == "dostawca")
                    {
                        kontrahenci = await kontrahenci.Where(k => k.Dostawca.Equals(true)).ToListAsync();
                    }
                    if (filtry.Rodzaj == "odbiorca")
                    {
                        kontrahenci = await kontrahenci.Where(k => k.Odbiorca.Equals(true)).ToListAsync();
                    }
                }
                if (filtry.Zalezny != null)
                {
                    kontrahenci = await kontrahenci.Where(k => k.Zalezny.Equals(bool.Parse(filtry.Zalezny))).ToListAsync();
                }
                if (filtry.PlatnikVat != null)
                {
                    kontrahenci = await kontrahenci.Where(k => k.PlatnikVat.Equals(bool.Parse(filtry.PlatnikVat))).ToListAsync();
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
                kontrahenci = kontrahenci
                    .Where(k => k.Nazwa.ToLower().Contains(pagingInfo.SearchString.ToLower())).ToList();
            }

            switch (pagingInfo.SortOrder)
            {
                case "name_desc":
                    kontrahenci = kontrahenci.OrderByDescending(k => k.Nazwa).ToList();
                    break;
                case "Name":
                    kontrahenci = kontrahenci.OrderBy(k => k.Nazwa).ToList();
                    break;
                case "vat_desc":
                    kontrahenci = kontrahenci.OrderByDescending(k => k.PlatnikVat).ToList();
                    break;
                case "Vat":
                    kontrahenci = kontrahenci.OrderBy(k => k.PlatnikVat).ToList();
                    break;
                case "odbiorca_desc":
                    kontrahenci = kontrahenci.OrderByDescending(k => k.Odbiorca).ToList();
                    break;
                case "Odbiorca":
                    kontrahenci = kontrahenci.OrderBy(k => k.Odbiorca).ToList();
                    break;
                case "dostawca_desc":
                    kontrahenci = kontrahenci.OrderByDescending(k => k.Dostawca).ToList();
                    break;
                case "Dostawca":
                    kontrahenci = kontrahenci.OrderBy(k => k.Dostawca).ToList();
                    break;
                case "zalezny_desc":
                    kontrahenci = kontrahenci.OrderByDescending(k => k.Zalezny).ToList();
                    break;
                case "Zalezny":
                    kontrahenci = kontrahenci.OrderBy(k => k.Zalezny).ToList();
                    break;
                case "bank_desc":
                    kontrahenci = kontrahenci.OrderByDescending(k => k.Bank).ToList();
                    break;
                case "Bank":
                    kontrahenci = kontrahenci.OrderBy(k => k.Bank).ToList();
                    break;
                default:
                    kontrahenci = kontrahenci.OrderByDescending(k => k.IdKontrahenta).ToList();
                    break;
            }
            if (pagingInfo.PageSize == 0)
            {
                pagingInfo.PageSize = 10;
            }

            var kontrahenciDto = _mapper.Map<List<KontrahentDto>>(kontrahenci);

            return await kontrahenciDto.ToPagedListAsync(page ?? 1, pagingInfo.PageSize);
        }
        #endregion
        #region GetKontrahentDto
        public async Task<KontrahentDto> GetKontrahentDto(int? id)
        {
            var kontrahent = await _dbContext
                .Kontrahenci
                .FirstOrDefaultAsync(k => k.IdKontrahenta == id);

            var kontrahentDto = _mapper.Map<KontrahentDto>(kontrahent);

            return kontrahentDto;
        }
        #endregion
        #region GetSzablony
        public async Task<IEnumerable<DokumentDto>> GetSzablony()
        {
            var dokumenty = await _dbContext
                .Dokumenty
                .ToListAsync();

            var dokumentyDto = _mapper.Map<List<DokumentDto>>(dokumenty);
            return dokumentyDto;
        }
        #endregion
        #region GetDokumenty
        public async Task<IPagedList<DokumentKontrahentaDto>> GetDokumenty(int id,int? page, PagingInfo pagingInfo)
        {
            var dokumentyKontrahenta = await _dbContext
                .DokumentyKontrahenta
                .Include(dk => dk.Kontrahent)
                .Where(dk=>dk.IdKontrahenta == id)
                .ToListAsync();

            switch (pagingInfo.SortOrder)
            {
                case "name_desc":
                    dokumentyKontrahenta = dokumentyKontrahenta.OrderByDescending(d => d.NazwaDokumentu).ToList();
                    break;
                case "Name":
                    dokumentyKontrahenta = dokumentyKontrahenta.OrderBy(d => d.NazwaDokumentu).ToList();
                    break;
                default:
                    dokumentyKontrahenta = dokumentyKontrahenta.OrderByDescending(d => d.IdDokumentuKontrahenta).ToList();
                    break;
            }
            if (pagingInfo.PageSize == 0)
            {
                pagingInfo.PageSize = 10;
            }

            var dokumentyKontrahentaDto = _mapper.Map<List<DokumentKontrahentaDto>>(dokumentyKontrahenta);

            return await dokumentyKontrahentaDto.ToPagedListAsync(page ?? 1, pagingInfo.PageSize);
        }
        #endregion
        #region AddDokument
        public async Task<FileDto> AddDokument(DokumentKontrahentaDto dto)
        {
            string fileName = null;
            string filePath = null;
            string uploadsFolder, uniqueFileName;

            var dokument =await _dbContext
                .Dokumenty
                .FindAsync(dto.IdSzablonu);

            var kontrahent = await _dbContext
                .Kontrahenci
                .Include(k=>k.Adres)
                .FirstOrDefaultAsync(k=>k.IdKontrahenta == dto.IdKontrahenta);

            var kontrahentDto = new KontrahentDto()
            {
                NipLubPesel = kontrahent.NipLubPesel ?? "-",
                Regon = kontrahent.Regon ?? "-",
                Nazwa = kontrahent.Nazwa ?? "-",
                SkrotNazwy = kontrahent.SkrotNazwy ?? "-",
                NumerKonta = kontrahent.NumerKonta ?? "-",
                Bank = kontrahent.Bank ?? "-",
                Ulica = kontrahent.Adres.Ulica ?? "-",
                Miasto = kontrahent.Adres.Miasto ?? "-",
                KodPocztowy = kontrahent.Adres.KodPocztowy ?? "-"
            };

            if (dokument != null)
            {
                uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedFiles");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + dokument.NazwaDokumentu +".doc";
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                fileName = dokument.NazwaDokumentu;
                GenerateDocument(dokument.UrlDokumentu,filePath, kontrahentDto);
            }

            var dokumentKontrahentaDto = new DokumentKontrahentaDto
            {
                IdKontrahenta = kontrahent.IdKontrahenta,
                NazwaDokumentu = fileName,
                UrlDokumentu = filePath
            };

            byte[] bytes = File.ReadAllBytes(dokument.UrlDokumentu);
            RunDocument(filePath);

            var dokumentKontrahenta = _mapper.Map<DokumentKontrahenta>(dokumentKontrahentaDto);
            _dbContext.DokumentyKontrahenta.Add(dokumentKontrahenta);
            await _dbContext.SaveChangesAsync();

            return new FileDto() { fileBytes = bytes, fileName = dokumentKontrahentaDto.NazwaDokumentu };
        }
        #endregion
        #region RunDocument
        private void RunDocument(string filePath)
        {
            Process process = new Process();
            ProcessStartInfo procesInfo = new ProcessStartInfo();
            procesInfo.UseShellExecute = true;
            procesInfo.FileName = filePath;
            process.StartInfo = procesInfo;
            process.Start();
            process.WaitForExit();
        }
        #endregion
        #region GenerateDokumentKontrahenta
        public void GenerateDocument(string urlDokumentu,string filePath,KontrahentDto dto)
        {
            Document doc = new Document(urlDokumentu);
            doc.Range.Replace("@NipLubPesel@", dto.NipLubPesel, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("@Regon@", dto.Regon, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("@Nazwa@", dto.Nazwa, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("@NumerKonta@", dto.NumerKonta, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("@Bank@", dto.Bank, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("@Ulica@", dto.Ulica, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("@KodPocztowy@", dto.KodPocztowy, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("@Miasto@", dto.Miasto, new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Save(filePath);
        }
        #endregion
        #region DownloadDokument
        public async Task<FileDto> DownloadDokument(int? id)
        {
            var dokumentKontrahenta = await _dbContext
                .DokumentyKontrahenta
                .FindAsync(id);
            byte[] bytes = File.ReadAllBytes(dokumentKontrahenta.UrlDokumentu);
            RunDocument(dokumentKontrahenta.UrlDokumentu);
            return new FileDto() { fileBytes = bytes, fileName = dokumentKontrahenta.NazwaDokumentu };
        }
        #endregion
    }
}