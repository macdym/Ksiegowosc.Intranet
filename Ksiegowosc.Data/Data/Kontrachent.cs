using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegowosc.Data.Data
{
    public class Kontrachent
    {
        [Key]
        public int IdKontrachenta { get; set; }
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
        public virtual ICollection<DokumentKontrachenta> DokumentKontrachenta{ get; set; }
    }
}
