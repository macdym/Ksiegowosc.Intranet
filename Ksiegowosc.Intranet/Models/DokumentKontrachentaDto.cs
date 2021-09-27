using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Models
{
    public class DokumentKontrachentaDto
    {
        public int IdDokumentu { get; set; }
        public int NazwaDokumentu { get; set; }
        public int UrlDokumentu { get; set; }
        public DateTime? DataWygenerowania { get; set; }
    }
}
