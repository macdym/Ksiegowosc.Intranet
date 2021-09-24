using AutoMapper;
using Ksiegowosc.Data;
using Ksiegowosc.Intranet.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Ksiegowosc.Intranet.Services
{
    public interface IDokumentService
    {
        Task<IPagedList<DokumentDto>> GetAll(int? page, PagingInfo pagingInfo);
    }

    public class DokumentService : IDokumentService
    {
        private readonly KsiegowoscDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DokumentService(KsiegowoscDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        #region GetAll
        public async Task<IPagedList<DokumentDto>> GetAll(int? page, PagingInfo pagingInfo)
        {
            var dokumenty = await _dbContext
                .Dokumenty
                .ToListAsync();

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
                dokumenty = dokumenty
                    .Where(d => d.NazwaDokumentu.ToLower().Contains(pagingInfo.SearchString.ToLower())).ToList();
            }

            switch (pagingInfo.SortOrder)
            {
                case "name_desc":
                    dokumenty = dokumenty.OrderByDescending(d => d.NazwaDokumentu).ToList();
                    break;
                case "Name":
                    dokumenty = dokumenty.OrderBy(d => d.NazwaDokumentu).ToList();
                    break;
                default:
                    dokumenty = dokumenty.OrderByDescending(d => d.IdDokumentu).ToList();
                    break;
            }
            if (pagingInfo.PageSize == 0)
            {
                pagingInfo.PageSize = 10;
            }

            var dokumentyDto = _mapper.Map<List<DokumentDto>>(dokumenty);

            return await dokumentyDto.ToPagedListAsync(page ?? 1, pagingInfo.PageSize);
        }
        #endregion
    }
}
