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
            CreateMap<Kontrahent, KontrahentDto>()
                .ForMember(dto => dto.Ulica, c => c.MapFrom(s => s.Adres.Ulica))
                .ForMember(dto => dto.Miasto, c => c.MapFrom(s => s.Adres.Miasto))
                .ForMember(dto => dto.KodPocztowy, c => c.MapFrom(s => s.Adres.KodPocztowy));
            CreateMap<KontrahentDto, Kontrahent>()
                .ForMember(m => m.Adres, c => c.MapFrom(dto => new Adres()
                {
                    Ulica = dto.Ulica,
                    Miasto = dto.Miasto,
                    KodPocztowy = dto.KodPocztowy
                }));
            CreateMap<Dokument, DokumentDto>();
            CreateMap<DokumentDto, Dokument>();
            CreateMap<DokumentKontrahentaDto,DokumentKontrahenta>();
            CreateMap<DokumentKontrahenta, DokumentKontrahentaDto>();
        }
    }
}
