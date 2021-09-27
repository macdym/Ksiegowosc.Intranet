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
using Ksiegowosc.Data.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Diagnostics;
using Aspose.Words;
using Aspose.Words.Replacing;

namespace Ksiegowosc.Intranet.Services
{
    public interface IKontrachentService
    {
        Task<IPagedList<KontrachentDto>> GetAll(int? page, PagingInfo pagingInfo, FiltryKontrachentaDto filtry);
        Task<KontrachentDto> GetKontrachentDto(int? id);
        Task Create(KontrachentDto dto);
        Task Update(KontrachentDto dto);
        Task Delete(int? id);
        Task<IPagedList<DokumentKontrachentaDto>> GetDokumenty(int id ,int? page, PagingInfo pagingInfo);
        Task<IEnumerable<DokumentDto>> GetSzablony();
        Task<FileDto> AddDokument(DokumentKontrachentaDto dto);
    }

    public class KontrachentService : IKontrachentService
    {
        private readonly KsiegowoscDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KontrachentService(KsiegowoscDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        #region Edit

        #endregion
        #region Delete
        public async Task Delete(int? id)
        {
            var kontrachent = await _dbContext
                .Kontrachenci
                .FirstOrDefaultAsync(k => k.IdKontrachenta == id);

            _dbContext.Kontrachenci.Remove(kontrachent);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
        #region Create
        public async Task Create(KontrachentDto dto)
        {
            var kontrachent = _mapper.Map<Kontrachent>(dto);
            await _dbContext.Kontrachenci.AddAsync(kontrachent);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
        #region Edit
        public async Task Update(KontrachentDto dto)
        {
            var kontrachentDto = _mapper.Map<Kontrachent>(dto);
            _dbContext.Update(kontrachentDto);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
        #region GetAll
        public async Task<IPagedList<KontrachentDto>> GetAll(int? page, PagingInfo pagingInfo, FiltryKontrachentaDto filtry)
        {
            var kontrachenci = await _dbContext
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
                        kontrachenci = await kontrachenci.Where(k => k.Odbiorca.Equals(true)).ToListAsync();
                    }
                }
                if (filtry.Zalezny != null)
                {
                    kontrachenci = await kontrachenci.Where(k => k.Zalezny.Equals(bool.Parse(filtry.Zalezny))).ToListAsync();
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
                    kontrachenci = kontrachenci.OrderByDescending(k => k.Nazwa).ToList();
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

            return await kontrachenciDto.ToPagedListAsync(page ?? 1, pagingInfo.PageSize);
        }
        #endregion
        #region GetKontrachentDto
        public async Task<KontrachentDto> GetKontrachentDto(int? id)
        {
            var kontrachent = await _dbContext
                .Kontrachenci
                .FirstOrDefaultAsync(k => k.IdKontrachenta == id);

            var kontrachentDto = _mapper.Map<KontrachentDto>(kontrachent);

            return kontrachentDto;
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
        public async Task<IPagedList<DokumentKontrachentaDto>> GetDokumenty(int id,int? page, PagingInfo pagingInfo)
        {
            var dokumentyKontrachenta = await _dbContext
                .DokumentyKontrachenta
                .Include(dk => dk.Kontrachent)
                .Where(dk=>dk.IdKontrachenta == id)
                .ToListAsync();

            switch (pagingInfo.SortOrder)
            {
                case "name_desc":
                    dokumentyKontrachenta = dokumentyKontrachenta.OrderByDescending(d => d.NazwaDokumentu).ToList();
                    break;
                case "Name":
                    dokumentyKontrachenta = dokumentyKontrachenta.OrderBy(d => d.NazwaDokumentu).ToList();
                    break;
                default:
                    dokumentyKontrachenta = dokumentyKontrachenta.OrderByDescending(d => d.IdDokumentuKontrachenta).ToList();
                    break;
            }
            if (pagingInfo.PageSize == 0)
            {
                pagingInfo.PageSize = 10;
            }

            var dokumentyKontrachentaDto = _mapper.Map<List<DokumentKontrachentaDto>>(dokumentyKontrachenta);

            return await dokumentyKontrachentaDto.ToPagedListAsync(page ?? 1, pagingInfo.PageSize);
        }
        #endregion
        #region AddDokument
        public async Task<FileDto> AddDokument(DokumentKontrachentaDto dto)
        {
            string fileName = null;
            string filePath = null;
            string uploadsFolder, uniqueFileName;

            var dokument =await _dbContext
                .Dokumenty
                .FindAsync(dto.IdSzablonu);

            var kontrachent = await _dbContext
                .Kontrachenci
                .Include(k=>k.Adres)
                .FirstOrDefaultAsync(k=>k.IdKontrachenta == dto.IdKontrachenta);

            var kontrachentDto = new KontrachentDto()
            {
                NipLubPesel = kontrachent.NipLubPesel ?? "-",
                Regon = kontrachent.Regon ?? "-",
                Nazwa = kontrachent.Nazwa ?? "-",
                SkrotNazwy = kontrachent.SkrotNazwy ?? "-",
                NumerKonta = kontrachent.NumerKonta ?? "-",
                Bank = kontrachent.Bank ?? "-",
                Ulica = kontrachent.Adres.Ulica ?? "-",
                Miasto = kontrachent.Adres.Miasto ?? "-",
                KodPocztowy = kontrachent.Adres.KodPocztowy ?? "-"
            };

            if (dokument != null)
            {
                uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedFiles");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + dokument.NazwaDokumentu +".doc";
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                fileName = dokument.NazwaDokumentu;
                GenerateDocument(dokument.UrlDokumentu,filePath, kontrachentDto);
            }

            var dokumentKontrachentaDto = new DokumentKontrachentaDto
            {
                IdKontrachenta = kontrachent.IdKontrachenta,
                NazwaDokumentu = fileName,
                UrlDokumentu = filePath
            };

            var dokumentKontrachenta = _mapper.Map<DokumentKontrachenta>(dokumentKontrachentaDto);
            _dbContext.DokumentyKontrachenta.Add(dokumentKontrachenta);
            await _dbContext.SaveChangesAsync();

            byte[] bytes = File.ReadAllBytes(dokument.UrlDokumentu);
            RunDocument(filePath);

            return new FileDto() { fileBytes = bytes, fileName = dokumentKontrachentaDto.NazwaDokumentu };
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
        #region GenerateDokumentKontrachenta
        public void GenerateDocument(string urlDokumentu,string filePath,KontrachentDto dto)
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
            var dokumentKontrachenta = await _dbContext
                .DokumentyKontrachenta
                .FindAsync(id);
            byte[] bytes = File.ReadAllBytes(dokumentKontrachenta.UrlDokumentu);
            RunDocument(dokumentKontrachenta.UrlDokumentu);
            return new FileDto() { fileBytes = bytes, fileName = dokumentKontrachenta.NazwaDokumentu };
        }
        #endregion
    }
}
