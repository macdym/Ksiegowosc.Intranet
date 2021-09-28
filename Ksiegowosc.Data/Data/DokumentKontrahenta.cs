using System;
using System.ComponentModel.DataAnnotations;

namespace Ksiegowosc.Data.Data
{
    public class DokumentKontrahenta
    {
        [Key]
        public int IdDokumentuKontrahenta { get; set; }
        public string NazwaDokumentu { get; set; }
        public string UrlDokumentu { get; set; }
        public DateTime? DataDodania { get; set; }
        public int IdKontrahenta { get; set; }
        public virtual Kontrahent Kontrahent { get; set; }
    }
}
