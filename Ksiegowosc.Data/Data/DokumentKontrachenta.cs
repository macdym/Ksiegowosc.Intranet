using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegowosc.Data.Data
{
    public class DokumentKontrachenta
    {
        [Key]
        public int IdDokumentuKontrachenta { get; set; }
        public string NazwaDokumentu { get; set; }
        public string UrlDokumentu { get; set; }
        public DateTime? DataDodania { get; set; }
        public int IdKontrachenta { get; set; }
        public virtual Kontrachent Kontrachent { get; set; }
    }
}
