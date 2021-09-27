using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Models
{
    public class DokumentKontrachentaDto
    {
        //public int IdDokumentuKontrachenta { get; set; }
        public int IdKontrachenta { get; set; }
        public int IdSzablonu { get; set; }
        public string NazwaDokumentu { get; set; }
        public string UrlDokumentu { get; set; }
        public DateTime? DataWygenerowania { get; set; }
    }
}
