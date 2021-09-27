using Ksiegowosc.Intranet.Models;
using X.PagedList;

namespace Ksiegowosc.Intranet.ViewModels
{
    public class KontrachentViewModel
    {
        public IPagedList<KontrachentDto> Kontrachenci { get; set; }
        public KontrachentDto KontrachentDto { get; set; }
        public CreateDokumentDto CreateDokumentDto { get; set; }
        public IPagedList<DokumentKontrachentaDto> DokumentyKontrachenta { get; set; }
    }
}
