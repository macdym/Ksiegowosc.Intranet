using Ksiegowosc.Intranet.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace Ksiegowosc.Intranet.ViewModels
{
    public class DokumentViewModel
    {
        public IPagedList<DokumentDto> Dokumenty { get; set; }
        public string NazwaDokumentu { get; set; }
        public IFormFile Dokument { get; set; }
    }
}
