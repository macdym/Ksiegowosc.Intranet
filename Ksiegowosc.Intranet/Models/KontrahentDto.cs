using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegowosc.Intranet.Models
{
    public class KontrahentDto
    {
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
        public string Ulica { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
    }
}
