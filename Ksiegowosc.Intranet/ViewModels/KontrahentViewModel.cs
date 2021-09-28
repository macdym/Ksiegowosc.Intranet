using Ksiegowosc.Intranet.Models;
using X.PagedList;

namespace Ksiegowosc.Intranet.ViewModels
{
    public class KontrahentViewModel
    {
        public IPagedList<KontrahentDto> Kontrahenci { get; set; }
        public IPagedList<DokumentKontrahentaDto> DokumentyKontrahenta { get; set; }
        public KontrahentDto KontrahentDto { get; set; }
        public DokumentKontrahentaDto DokumentKontrahentaDto { get; set; }
    }
}
