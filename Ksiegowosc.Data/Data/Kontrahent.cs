using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksiegowosc.Data.Data
{
    public class Kontrahent
    {
        [Key]
        public int IdKontrahenta { get; set; }
        public string NipLubPesel { get; set; }
        public string Regon { get; set; }
        public bool PlatnikVat { get; set; }
        public string Nazwa { get; set; }
        public string SkrotNazwy { get; set; }
        public bool Dostawca { get; set; }
        public bool Odbiorca { get; set; }
        public bool Zalezny { get; set; }
        public string Bank { get; set; }
        public string NumerKonta { get; set; }
        public int IdAdresu { get; set; }
        public virtual Adres Adres { get; set; }
        public virtual ICollection<DokumentKontrahenta> DokumentKontrahenta{ get; set; }
    }
}
