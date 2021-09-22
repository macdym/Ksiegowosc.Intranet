using AutoMapper;
using Ksiegowosc.Data.Data;
using Ksiegowosc.Intranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet
{
    public class KsiegowoscMappingProfile : Profile
    {
        public KsiegowoscMappingProfile()
        {
            CreateMap<Kontrachent, KontrachentDto>()
                .ForMember(m => m.Ulica, c => c.MapFrom(s => s.Adres.Ulica))
                .ForMember(m => m.Miasto, c => c.MapFrom(s => s.Adres.Miasto))
                .ForMember(m => m.KodPocztowy, c => c.MapFrom(s => s.Adres.KodPocztowy));
        }
    }
}
