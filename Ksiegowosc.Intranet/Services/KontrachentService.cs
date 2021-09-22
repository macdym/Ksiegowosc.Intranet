using AutoMapper;
using Ksiegowosc.Data;
using Ksiegowosc.Data.Data;
using Ksiegowosc.Intranet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Services
{
    public interface IKontrachentService
    {
        IEnumerable<KontrachentDto> GetKontrachenci();
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
        public IEnumerable<KontrachentDto> GetKontrachenci()
        {
            var kontrachenci = _dbContext
                .Kontrachenci
                .Include(k => k.Adres)
                .ToList();

            var kontrachenciDto = _mapper.Map<List<KontrachentDto>>(kontrachenci);
            return kontrachenciDto;
        }
    }
}
