using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Models
{
    public class DokumentKontrahentaDto
    {
        public int IdDokumentuKontrahenta { get; set; }
        public int IdKontrahenta { get; set; }
        public int IdSzablonu { get; set; }
        public string NazwaDokumentu { get; set; }
        public string UrlDokumentu { get; set; }
        public DateTime? DataWygenerowania { get; set; }
    }
}
